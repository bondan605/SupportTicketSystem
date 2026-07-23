//using SupportTicketSystem.Application.Interfaces.Repositories;
//using SupportTicketSystem.Domain.Entities;
//using SupportTicketSystem.Infrastructure.Persistence.Repositories;

//namespace SupportTicketSystem.Infrastructure.Persistence
//{
//    public class UnitOfWork : IUnitOfWork
//    {
//        private readonly AppDbContext _context;

//        private IAccountRepository? _accounts;
//        private ICategoryRepository? _categories;
//        private ITransactionRepository? _transactions;
        
//        public UnitOfWork(AppDbContext context) 
//        {
//            _context = context;
//        }

//        // --- Repository Access ---
//        // Each property initializes the repository on first access,
//        // passing the shared _context instance into it.
//        // This ensures all repositories share the same change tracker.
//        public IAccountRepository Accounts => _accounts ??= new AccountRepository(_context);
//        public ICategoryRepository Categories => _categories ??= new CategoryRepository(_context);
//        public ITransactionRepository Transactions => _transactions ??= new TransactionRepository(_context);

//        // --- Commit ---
//        // One call persists all tracked changes from all repositories.
//        // This is the entire point of Unit of Work.
//        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);

//        // --- Cleanup ---
//        // Disposing UnitOfWork disposes the DbContext.
//        public void Dispose() => _context.Dispose();
//    }
//}
