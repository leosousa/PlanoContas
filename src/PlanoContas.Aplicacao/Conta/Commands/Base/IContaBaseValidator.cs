namespace PlanoContas.Aplicacao.Conta.Commands.Base
{
    public interface IContaBaseValidator
    {
        bool CodigoPrecisaSerUnicoNoCadastro(string codigo);
        bool CodigoPrecisaSerUnicoNaEdicao(ContaCommand command);
        bool CodigoPrecisaSerValido(string codigo);
        bool ContaPaiPrecisaEstarCadastrada(int? idContaPai);
        bool ContaPaiPodeTerContasFilhasAssociadas(int? idContaPai);
        bool ContaNovaPrecisaSerDoMesmoTipoQueContaPai(ContaCommand command);
        bool TipoReceitaPrecisaSerValido(ContaCommand command);
        bool ContaJaExistente(int idConta);
        bool ContaNaoPossuiFilhos(int idConta);
    }
}
