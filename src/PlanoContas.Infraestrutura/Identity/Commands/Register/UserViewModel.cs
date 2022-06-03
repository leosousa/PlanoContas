using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Infraestrutura.Identity.Commands.Register
{
    public class UserRegisterResult
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UserRegisterViewModel
    {
        public Result? Result { get; set; }
        public UserRegisterResult? User { get; set; }
    }
}
