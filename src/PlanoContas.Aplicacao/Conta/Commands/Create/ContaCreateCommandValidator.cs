using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Aplicacao.Conta.Commands.Base;
using PlanoContas.Dominio.Enumeracoes;

namespace PlanoContas.Aplicacao.Conta.Commands.Conta.Create
{
    public class ContaCreateCommandValidator : AbstractValidator<ContaCreateCommand>
    {
        private readonly IContaBaseValidator _baseValidator;

        public ContaCreateCommandValidator(
            IContaBaseValidator baseValidator
        )
        {
            _baseValidator = baseValidator;

            RuleFor(v => v.Codigo)
                .NotEmpty()
                    .WithMessage("Código é campo obrigatório.")
                .MaximumLength(255)
                    .WithMessage("Código não pode ultrapassar 255 caracteres.")
                .Must(_baseValidator.CodigoPrecisaSerUnicoNoCadastro)
                    .WithMessage("Código já cadastrado.")
                .Must(_baseValidator.CodigoPrecisaSerValido)
                    .WithMessage("Código inválido.");

            RuleFor(v => v.Nome)
                .NotEmpty()
                    .WithMessage("Nome é campo obrigatório.")
                .MaximumLength(255)
                    .WithMessage("Nome não pode ultrapassar 255 caracteres.");

            RuleFor(v => v)
                .Must(_baseValidator.TipoReceitaPrecisaSerValido)
                    .WithMessage("Tipo de conta precisa ser válido.")
                .Must(_baseValidator.ContaNovaPrecisaSerDoMesmoTipoQueContaPai)
                    .WithMessage("Tipo de conta precisa ser do mesmo tipo de conta da conta pai.");

            RuleFor(v => v.IdContaPai)
                .Must(_baseValidator.ContaPaiPrecisaEstarCadastrada)
                    .WithMessage("Conta pai não cadastrada.")
                .Must(_baseValidator.ContaPaiPodeTerContasFilhasAssociadas)
                    .WithMessage("Conta pai inválida, pois uma conta que aceita lançamentos não pode ter contas filhas.");
        }
    }
}
