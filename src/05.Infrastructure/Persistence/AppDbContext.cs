using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Base.Entities;
using SupportTicketSystem.Domain.Entities;

namespace SupportTicketSystem.Infrastructure.Persistence
{
    /// <summary>
    /// The application's Entity Framework Core database context. 
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<TicketHistory> TicketHistories => Set<TicketHistory>();

        /// <summary>
        /// Builds the EF Core model. Entity configurations (schema constraints, indexes, relationships, and seed data) are defined separately per entity as
        /// <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{TEntity}"/> classes (e.g. UserConfiguration, TicketConfiguration, TicketHistoryConfiguration)
        /// and are discovered and applied automatically here via <see cref="ApplyConfigurationsFromAssembly"/>
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }

        /// <summary>
        /// Populates audit fields for tracked entities before they are saved.
        /// - Added entities get CreatedAt/CreatedBy filled in.
        /// - Modified entities get UpdatedAt/UpdatedBy filled in.
        /// </summary>
        private void ApplyAuditInformation()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    // For newly added entities
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy ??= Guid.Empty;
                        break;

                    // For entities being updated
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy ??= Guid.Empty;
                        break;
                }
            }
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