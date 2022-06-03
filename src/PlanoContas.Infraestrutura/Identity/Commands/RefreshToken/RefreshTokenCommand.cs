using MediatR;
using PlanoContas.Infraestrutura.Identity.Commands.Login;

namespace PlanoContas.Infraestrutura.Identity.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<TokenViewModel>
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
