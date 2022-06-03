using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Infraestrutura.Identity.Commands.Login
{
    [Serializable]
    public class TokenResult
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }

    [Serializable]
    public class TokenViewModel
    {
        public Result? Result { get; set; }
        public TokenResult? Token { get; set; }
    }
}
