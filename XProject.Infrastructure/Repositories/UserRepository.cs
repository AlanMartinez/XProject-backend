using Microsoft.EntityFrameworkCore;
using XProject.Core.Entities;
using XProject.Core.Interfaces;

namespace XProject.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
