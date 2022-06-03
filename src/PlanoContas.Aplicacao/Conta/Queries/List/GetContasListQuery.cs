using MediatR;
using PlanoContas.Aplicacao.Conta.Queries.Detail;

namespace PlanoContas.Aplicacao.Conta.Queries.List
{
    public record GetContasListQuery : IRequest<ContaListViewModel>;
}
