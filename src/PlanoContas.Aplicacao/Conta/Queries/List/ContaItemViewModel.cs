using PlanoContas.Aplicacao.Base.Mappings;
using PlanoContas.Dominio.Enumeracoes;
using Entidades = PlanoContas.Dominio.Entidades;

namespace PlanoContas.Aplicacao.Conta.Queries.Detail
{
    public class ContaItemViewModel : IMapFrom<Entidades.Conta>
    {
        public ContaItemViewModel()
        {
        }

        /// <summary>
        /// Identificador da conta
        /// </summary>
        public int Id { get; set; }

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
        public ContaItemViewModel? ContaPai { get; set; }

        /// <summary>
        /// Filhos
        /// </summary>
        public List<ContaItemViewModel> Filhos { get; set; }
    }
}
