using FluentValidation;
using PlanoContas.Aplicacao.Conta.Commands.Base;

namespace PlanoContas.Aplicacao.Conta.Commands.Delete
{
    public class ContaDeleteCommandValidator : AbstractValidator<ContaDeleteCommand>
    {
        private readonly IContaBaseValidator _baseValidator;

        public ContaDeleteCommandValidator(IContaBaseValidator baseValidator)
        {
            _baseValidator = baseValidator;

            var contaExistente = _baseValidator.ContaJaExistente;

            RuleFor(v => v.Id)
                .Must(_baseValidator.ContaJaExistente)
                    .WithMessage("Conta não encontrada.");
        }
    }
}
