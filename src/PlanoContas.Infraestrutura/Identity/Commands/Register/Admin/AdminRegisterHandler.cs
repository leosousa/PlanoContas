using MediatR;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Base.Results;
using PlanoContas.Infraestrutura.Identity.Commands.Register.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoContas.Infraestrutura.Identity.Commands.Register.Admin
{
    public class AdminRegisterHandler : IRequestHandler<AdminRegisterCommand, UserRegisterViewModel>
    {
        private readonly IIdentityService _identityService;

        public AdminRegisterHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserRegisterViewModel> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
        {
            var userRegister = await _identityService.CreateAdminUserAsync(request.Email!, request.Password!);

            if (userRegister.Result == null || string.IsNullOrWhiteSpace(userRegister.UserId))
            {
                return await Task.FromResult(new UserRegisterViewModel());
            }

            var result = new UserRegisterViewModel
            {
                Result = userRegister.Result,
                User = new UserRegisterResult
                {
                    Id = userRegister.UserId
                }
            };

            return await Task.FromResult(result);
        }
    }
}
