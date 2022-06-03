using MediatR;
using PlanoContas.Aplicacao.Base.Interfaces;
using Entidades = PlanoContas.Dominio.Entidades;

namespace PlanoContas.Aplicacao.Conta.Commands.Conta.Create
{
    public class ContaCreateCommandHandler : IRequestHandler<ContaCreateCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public ContaCreateCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(ContaCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = new Entidades.Conta();

            entity.Codigo = request.Codigo;
            entity.Nome = request.Nome;
            entity.TipoConta = request.TipoConta;
            entity.AceitaLancamentos = request.AceitaLancamentos;
            entity.IdContaPai = request.IdContaPai;

            _context.Contas.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
