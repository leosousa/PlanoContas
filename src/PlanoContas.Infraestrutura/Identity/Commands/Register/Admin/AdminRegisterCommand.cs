using MediatR;
using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Infraestrutura.Identity.Commands.Register.Admin
{
    public record AdminRegisterCommand : IRequest<UserRegisterViewModel>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
