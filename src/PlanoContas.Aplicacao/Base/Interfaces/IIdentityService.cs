using PlanoContas.Aplicacao.Base.Results;

namespace PlanoContas.Aplicacao.Base.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<(Result Result, string UserId)> CreateAdminUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
