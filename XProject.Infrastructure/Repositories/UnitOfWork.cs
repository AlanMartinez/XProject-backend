using XProject.Core.Interfaces;

namespace XProject.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ISecurityRepository _securityRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(AppDbContext context, ISecurityRepository securityRepository, IUserRepository userRepository)
        {
            _context = context;
            _securityRepository = securityRepository;
            _userRepository = userRepository;
        }

        public ISecurityRepository SecurityRepository => _securityRepository ?? new SecurityRepository(_context);
        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_context);
        //public IRepository<Operation> OperationRepository => _operationRepository ?? new BaseRepository<Operation>(_context);

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}