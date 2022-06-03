using MediatR;
using PlanoContas.Aplicacao.Conta.Commands.Base;

namespace PlanoContas.Aplicacao.Conta.Commands.Edit
{
    public record ContaEditCommand : ContaCommand, IRequest
    {
        public int Id { get; set; }
    }
}
