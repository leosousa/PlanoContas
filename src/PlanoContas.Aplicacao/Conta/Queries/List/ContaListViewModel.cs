using PlanoContas.Aplicacao.Conta.Queries.Detail;

namespace PlanoContas.Aplicacao.Conta.Queries.List
{
    public class ContaListViewModel
    {
        public IList<ContaItemViewModel> List { get; set; } = new List<ContaItemViewModel>();
    }
}
