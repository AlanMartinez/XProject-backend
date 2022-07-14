namespace XProject.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISecurityRepository SecurityRepository { get; }
        IUserRepository UserRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}