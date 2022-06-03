using PlanoContas.Dominio.Entidades.Base;
using PlanoContas.Dominio.Enumeracoes;

namespace PlanoContas.Dominio.Entidades
{
    public class Conta : BaseEntity
    {
        /// <summary>
        /// Código da conta
        /// </summary>
        public string? Codigo { get; set; }

        /// <summary>
        /// Nome da conta
        /// </summary>
        public string? Nome { get; set; }

        /// <summary>
        /// Aceita lançamentos na conta
        /// </summary>
        public bool AceitaLancamentos { get; set; }

        /// <summary>
        /// Tipo de conta associado à conta
        /// </summary>
        public ETipoConta TipoConta { get; set; }

        /// <summary>
        /// Conta-pai desta conta
        /// </summary>
        public Conta? ContaPai { get; set; }
        public int? IdContaPai { get; set; }

        /// <summary>
        /// Contas-filhas associadas
        /// </summary>
        public IList<Conta> Filhos { get; set; } = new List<Conta>();
    }
}
