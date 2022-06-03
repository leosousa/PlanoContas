using MediatR;

namespace PlanoContas.Aplicacao.Conta.Commands.Delete
{
    public record ContaDeleteCommand(int Id) : IRequest;
}
