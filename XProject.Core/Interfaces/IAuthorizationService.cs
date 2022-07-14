using XProject.Core.Entities;

namespace XProject.Core.Interfaces
{
    public interface IAuthorizationService
    {
        Task<Security> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(Security security);
        string GenerateToken(Security security);
        Task<(bool, Security)> IsValidUser(UserLogin userLogin);

    }
}
