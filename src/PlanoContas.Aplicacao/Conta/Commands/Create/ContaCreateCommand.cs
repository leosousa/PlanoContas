using MediatR;
using PlanoContas.Aplicacao.Conta.Commands.Base;
using PlanoContas.Dominio.Enumeracoes;

namespace PlanoContas.Aplicacao.Conta.Commands.Conta.Create
{
    public record ContaCreateCommand : ContaCommand, IRequest<int>
    {
    }
}
