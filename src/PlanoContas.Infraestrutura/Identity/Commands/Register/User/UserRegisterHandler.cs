using MediatR;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Infraestrutura.Identity.Commands.Register.User
{
    public class UserRegisterHandler : IRequestHandler<UserRegisterCommand, UserRegisterViewModel>
    {
        private readonly IIdentityService _identityService;

        public UserRegisterHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserRegisterViewModel> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            var userRegister = await _identityService.CreateUserAsync(request.Email!, request.Password!);

            if (userRegister.Result == null || string.IsNullOrWhiteSpace(userRegister.UserId))
            {
                return await Task.FromResult(new UserRegisterViewModel());
            }

            var result = new UserRegisterViewModel { 
                Result = userRegister.Result,
                User = new UserRegisterResult { 
                    Id = userRegister.UserId
                }
            };

            return await Task.FromResult(result);
        }
    }
}
