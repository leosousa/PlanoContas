using PlanoContas.Aplicacao.Base.Mappings;
using PlanoContas.Dominio.Enumeracoes;
using Entidades = PlanoContas.Dominio.Entidades;

namespace PlanoContas.Aplicacao.Conta.Queries.Detail
{
    public class ContaDetailViewModel : IMapFrom<Entidades.Conta>
    {
        public ContaDetailViewModel()
        {
            Filhos = new List<ContaItemViewModel>();
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
        /// Contas-filhas associadas
        /// </summary>
        public IList<ContaItemViewModel> Filhos { get; private set; }
    }
}
