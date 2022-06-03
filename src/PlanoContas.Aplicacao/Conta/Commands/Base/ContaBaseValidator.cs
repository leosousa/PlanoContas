using PlanoContas.Aplicacao.Base.Interfaces;
using PlanoContas.Dominio.Enumeracoes;

namespace PlanoContas.Aplicacao.Conta.Commands.Base
{
    public class ContaBaseValidator : IContaBaseValidator
    {
        public readonly IApplicationDbContext _context;

        public ContaBaseValidator(IApplicationDbContext context)
        {
            _context = context;
        }

        public bool CodigoPrecisaSerUnicoNoCadastro(string codigo)
        {
            return !_context.Contas
                .Any(l => l.Codigo == codigo);
        }

        public bool CodigoPrecisaSerUnicoNaEdicao(ContaCommand command)
        {
            var contas = _context.Contas
                .Where(l => l.Codigo == command.Codigo)
                .ToList();

            return (contas == null || !contas.Any() || contas.Count == 1);
        }

        public bool CodigoPrecisaSerValido(string codigo)
        {
            // Validando código vazio ou nulo
            if (string.IsNullOrWhiteSpace(codigo)) return false;

            // Validando partes do código para saber se todas são números
            var codigoPartes = codigo.Split(".");
            var auxiliarConversao = 0;
            if (codigoPartes.Length >= 2)
            {
                foreach (var codigoParte in codigoPartes)
                {
                    if (!int.TryParse(codigoParte, out auxiliarConversao))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (!int.TryParse(codigo, out auxiliarConversao))
                {
                    return false;
                }
            }

            return true;
        }

        public bool ContaPaiPrecisaEstarCadastrada(int? idContaPai)
        {
            if (idContaPai is null || !idContaPai.HasValue) return true;

            return _context.Contas
                .Any(l => l.Id == idContaPai);
        }

        public bool ContaPaiPodeTerContasFilhasAssociadas(int? idContaPai)
        {
            if (idContaPai is null || !idContaPai.HasValue) return true;

            var conta = _context.Contas
                .FirstOrDefault(l => l.Id == idContaPai);

            if (conta == null) return true;

            return !conta.AceitaLancamentos;
        }

        public bool ContaNovaPrecisaSerDoMesmoTipoQueContaPai(ContaCommand command)
        {
            if (command.IdContaPai is null || !command.IdContaPai.HasValue) return true;

            return _context.Contas
                .Any(l => l.Id == command.IdContaPai && l.TipoConta == command.TipoConta);
        }

        public bool TipoReceitaPrecisaSerValido(ContaCommand command)
        {
            return Enum.GetValues(typeof(ETipoConta)).Cast<ETipoConta>().Contains(command.TipoConta);
        }

        public bool ContaJaExistente(int idConta)
        {
            return _context.Contas
                .Any(l => l.Id == idConta);
        }

        public bool ContaNaoPossuiFilhos(int idConta)
        {
            return _context.Contas
                .Any(l => l.Id == idConta && !l.Filhos.Any());
        }
    }
}
