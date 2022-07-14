using Microsoft.EntityFrameworkCore;
using XProject.Core.Entities;
using XProject.Core.Interfaces;

namespace XProject.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        protected DbSet<T> _entities;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }
        public async Task<T> GetById(int id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<T> GetById(string id)
        {
            return await _entities.FirstOrDefaultAsync(x => Convert.ToString(x.Id) == id);
        }
        public async Task<T> Add(T entity)
        {
            await _entities.AddAsync(entity);

            return entity;
            
        }
        public async Task Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
