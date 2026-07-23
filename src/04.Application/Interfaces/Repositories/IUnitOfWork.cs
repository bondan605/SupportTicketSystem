namespace SupportTicketSystem.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITicketRepository Tickets { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
