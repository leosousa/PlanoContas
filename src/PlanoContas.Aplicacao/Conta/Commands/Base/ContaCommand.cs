using PlanoContas.Dominio.Enumeracoes;

namespace PlanoContas.Aplicacao.Conta.Commands.Base
{
    public record ContaCommand
    {
        public string? Codigo { get; set; }

        public string? Nome { get; set; }

        public bool AceitaLancamentos { get; set; }

        public ETipoConta TipoConta { get; set; }

        public int? IdContaPai { get; set; }
    }
}
