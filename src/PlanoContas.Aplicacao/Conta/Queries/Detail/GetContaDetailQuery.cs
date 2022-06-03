using MediatR;

namespace PlanoContas.Aplicacao.Conta.Queries.Detail
{
    public record GetContaDetailQuery : IRequest<ContaDetailViewModel>
    {
        public int? Id { get; set; }
    }
}
