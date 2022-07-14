using XProject.Core.Entities;

namespace XProject.Core.Interfaces
{
    public interface ISecurityRepository : IRepository<Security>
    {
        public Task<Security> GetLoginByCredentials(UserLogin userLogin);
        public Task<Security> GetUserBy(string userName);
    }
}
