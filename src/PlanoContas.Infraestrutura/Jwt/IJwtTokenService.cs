using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PlanoContas.Infraestrutura.Jwt
{
    public interface IJwtTokenService
    {
        Task<JwtSecurityToken> GenerateToken(IList<Claim> claimList);
        Task<string> GenerateRefreshToken();
    }
}
