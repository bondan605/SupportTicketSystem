using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Base.Entities;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.Extensions;

namespace SupportTicketSystem.Infrastructure.Persistence
{
    /// <summary>
    /// The application's Entity Framework Core database context. 
    /// </summary>
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketHistory> TicketHistories => Set<TicketHistory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }

        /// <summary>
        /// Populates audit fields for tracked entities before they are saved.
        /// </summary>
        private void ApplyAuditInformation()
        {
            var currentUserId = GetCurrentUserId();
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    // For newly added entities
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = currentUserId;
                        break;

                    // For entities being updated
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = currentUserId;
                        break;
                }
            }
        }

        /// <summary>
        /// Reads the current user's Id from the authenticated request's claims.
        /// (e.g. a background job with no request at all).
        /// </summary>
        private Guid GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User.GetUserId() ?? Guid.Empty;
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            ApplyAuditInformation();
            return base.SaveChanges();
        }

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditInformation();
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}