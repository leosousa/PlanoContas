using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanoContas.Aplicacao.Base.Exceptions;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Conta.Commands.Base;

namespace PlanoContas.Aplicacao.Conta.Commands.Delete
{
    public class ContaDeleteCommandHandler : IRequestHandler<ContaDeleteCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IContaBaseValidator _validator;

        public ContaDeleteCommandHandler(
            IApplicationDbContext context, 
            IContaBaseValidator validator
        )
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(ContaDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contas
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Conta), request.Id);
            }

            if (!_validator.ContaNaoPossuiFilhos(request.Id))
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Filhos", "Conta possui filhos.") });
            }

            _context.Contas.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
