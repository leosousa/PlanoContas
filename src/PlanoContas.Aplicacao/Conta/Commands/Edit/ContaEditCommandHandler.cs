using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanoContas.Aplicacao.Base.Exceptions;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Conta.Commands.Base;
using Entidades = PlanoContas.Dominio.Entidades;

namespace PlanoContas.Aplicacao.Conta.Commands.Edit
{
    public class ContaEditCommandHandler : IRequestHandler<ContaEditCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IContaBaseValidator _validator;

        public ContaEditCommandHandler(
            IApplicationDbContext context, 
            IContaBaseValidator validator
        )
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(ContaEditCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contas
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Entidades.Conta), request.Id);
            }

            if (!_validator.ContaNaoPossuiFilhos(request.Id))
            {
                throw new ValidationException(new List<ValidationFailure>() { new ValidationFailure("Filhos", "Conta possui filhos.") });
            }

            if (request.IdContaPai.HasValue)
            {
                var contaPai = await _context.Contas
                .FirstOrDefaultAsync(c => c.IdContaPai == request.IdContaPai, cancellationToken);

                if (contaPai == null)
                {
                    throw new NotFoundException(nameof(Entidades.Conta), request.IdContaPai.Value);
                }

                entity.IdContaPai = contaPai.Id;
            }
            
            entity.AceitaLancamentos = request.AceitaLancamentos;
            entity.Codigo = request.Codigo;
            entity.Nome = request.Nome;
            entity.Codigo = request.Codigo;
            entity.TipoConta = request.TipoConta;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
