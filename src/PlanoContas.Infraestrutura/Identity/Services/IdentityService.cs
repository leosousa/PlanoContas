using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Base.Results;
using PlanoContas.Infraestrutura.Identity.Commands.Login;
using PlanoContas.Infraestrutura.Identity.Constantes;
using PlanoContas.Infraestrutura.Identity.Entidades;
using PlanoContas.Infraestrutura.Identity.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlanoContas.Infraestrutura.Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<UsuarioAplicacao> _signInManager;
        private readonly IUserClaimsPrincipalFactory<UsuarioAplicacao> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<UsuarioAplicacao> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<UsuarioAplicacao> signInManager,
            IUserClaimsPrincipalFactory<UsuarioAplicacao> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService
            )
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new UsuarioAplicacao
            {
                UserName = userName,
                Email = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<(Result Result, string UserId)> CreateAdminUserAsync(string userName, string password)
        {
            var user = new UsuarioAplicacao
            {
                UserName = userName,
                Email = userName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!await _roleManager.RoleExistsAsync(UserRoles.Administrator))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Administrator));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Administrator))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Administrator);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(UsuarioAplicacao user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
