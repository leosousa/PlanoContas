using MediatR;

namespace PlanoContas.Infraestrutura.Identity.Commands.Register.User
{
    public record UserRegisterCommand : IRequest<UserRegisterViewModel>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
