using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlanoContas.Aplicacao.Base.Results;
using PlanoContas.Infraestrutura.Identity.Commands.Login;
using PlanoContas.Infraestrutura.Identity.Entidades;
using PlanoContas.Infraestrutura.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlanoContas.Infraestrutura.Identity.Commands.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, TokenViewModel>
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtTokenService _tokenService;

        public RefreshTokenHandler(
            UserManager<UsuarioAplicacao> userManager, 
            IConfiguration configuration, 
            IJwtTokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<TokenViewModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null || principal.Identity == null)
            {
                return await Task.FromResult(new TokenViewModel
                {
                    Result = Result.Failure(new string[] { "Token inválido ou token de atualização." })
                });
            }

            string username = principal.Identity.Name!;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return await Task.FromResult(new TokenViewModel
                {
                    Result = Result.Failure(new string[] { "Token inválido ou token de atualização." })
                });
            }

            var newAccessToken = await _tokenService.GenerateToken(principal.Claims.ToList());
            var newRefreshToken = await _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            var tokenViewModel = new TokenResult()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };

            return new TokenViewModel
            {
                Result = Result.Success(),
                Token = tokenViewModel
            };
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;

        }
    }
}
