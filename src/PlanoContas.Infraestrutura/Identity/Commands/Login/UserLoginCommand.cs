using MediatR;
using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Infraestrutura.Identity.Commands.Login
{
    public class UserLoginCommand : IRequest<TokenViewModel>
    {
        public string? Login { get; set; }

        public string? Password { get; set; }
    }
}
