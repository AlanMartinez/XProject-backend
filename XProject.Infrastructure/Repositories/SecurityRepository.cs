using Microsoft.EntityFrameworkCore;
using XProject.Core.Entities;
using XProject.Core.Interfaces;

namespace XProject.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _entities.FirstOrDefaultAsync(x => x.UserName == userLogin.UserName);
        }

        public async Task<Security> GetUserBy(string userName)
        {
            return await _entities.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
