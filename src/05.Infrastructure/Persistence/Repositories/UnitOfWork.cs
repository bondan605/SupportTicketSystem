using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Infrastructure.Repositories;

namespace SupportTicketSystem.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IUserRepository? _users;
        private ITicketRepository? _tickets;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _users ??= new UserRepository(_context);
        public ITicketRepository Tickets => _tickets ??= new TicketRepository(_context);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
        public void Dispose() => _context.Dispose();
    }
}
