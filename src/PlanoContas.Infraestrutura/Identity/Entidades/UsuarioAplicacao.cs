using Microsoft.AspNetCore.Identity;

namespace PlanoContas.Infraestrutura.Identity.Entidades
{
    public class UsuarioAplicacao : IdentityUser
    {
        /// <summary>
        /// Token de atualização
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Tempo de expiração do token de atualização
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
