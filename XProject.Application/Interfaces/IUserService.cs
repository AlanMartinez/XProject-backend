using XProject.Core.Entities;

namespace XProject.Application.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetCurrentUser();
    }
}
