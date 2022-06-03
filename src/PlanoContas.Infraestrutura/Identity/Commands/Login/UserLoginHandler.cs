using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanoContas.Aplicacao.Base.Exceptions;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Base.Results;
using PlanoContas.Infraestrutura.Identity.Entidades;
using PlanoContas.Infraestrutura.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PlanoContas.Infraestrutura.Identity.Commands.Login
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, TokenViewModel>
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        private readonly SignInManager<UsuarioAplicacao> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _tokenService;

        public UserLoginHandler(
            UserManager<UsuarioAplicacao> userManager,
            SignInManager<UsuarioAplicacao> signInManager,
            IConfiguration configuration,
            IJwtTokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<TokenViewModel> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            if (user == null)
            {
                return new TokenViewModel { 
                    Result = Result.Failure(new string[] { "Usuário não encontrado." }) 
                };
            }

            var signinResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (signinResult == null || !signinResult.Succeeded)
            {
                return new TokenViewModel
                {
                    Result = Result.Failure(new string[] { "Usuário não encontrado." })
                };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = await _tokenService.GenerateToken(authClaims);
            var refreshToken = await _tokenService.GenerateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);

            var tokenViewModel = new TokenResult()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };

            return new TokenViewModel
            {
                Result = Result.Success(),
                Token = tokenViewModel
            };
        }
    }
}
