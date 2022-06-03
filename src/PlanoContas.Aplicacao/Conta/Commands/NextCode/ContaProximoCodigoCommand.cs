using MediatR;

namespace PlanoContas.Aplicacao.Conta.Commands.NextCode
{
    public record ContaProximoCodigoCommand : IRequest<NextCodeViewModel>
    {
        public string? CodigoPai { get; set; }
    }
}
