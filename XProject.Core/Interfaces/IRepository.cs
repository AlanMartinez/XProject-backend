using XProject.Core.Entities;

namespace XProject.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity 
    {
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
        Task<T> GetById(string id);
        Task<T> Add(T entity);
        Task Update(T entity);
    }
}
