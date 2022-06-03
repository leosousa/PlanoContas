using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Aplicacao.Conta.Commands.NextCode
{
    [Serializable]
    public class NextCodeViewModel
    {
        public Result? Result { get; set; }
        public string ProximoCodigo { get; set; }
    }
}
