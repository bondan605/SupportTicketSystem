using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Infrastructure.Persistence;

namespace SupportTicketSystem.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<User>> GetAllAsync()
        //{
        //    return await _context.Users.AsNoTracking().ToListAsync();
        //}

        public async Task<(IEnumerable<User> Items, int TotalCount)> GetPagedUsersAsync(
            int pageNumber,
            int pageSize,
            string currentUserRole,
            string? searchString,
            UserRole? role,
            bool? status,
            DateTime? startDate,
            DateTime? endDate)
        {
            var query = _context.Users.AsQueryable();

            // hak akses role (Manager hanya melihat SupportAgent)
            if (currentUserRole == UserRole.Manager.ToString())
            {
                query = query.Where(u => u.Role == UserRole.SupportAgent);
            }

            // Filter Search (Nama, Username, Email, JobTitle)
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                var search = searchString.Trim().ToLower();
                query = query.Where(u => u.Name.ToLower().Contains(search) ||
                                         u.Username.ToLower().Contains(search) ||
                                         u.Email.ToLower().Contains(search) ||
                                         (u.JobTitle != null && u.JobTitle.ToLower().Contains(search)));
            }

            // Filter berdasarkan Role
            if (role.HasValue)
            {
                query = query.Where(u => u.Role == role.Value);
            }

            // Filter berdasarkan Status Aktif/Non-Aktif
            if (status.HasValue)
            {
                query = query.Where(u => u.IsActive == status.Value);
            }

            // Filter berdasarkan Tanggal Dibuat (Start Date & End Date)
            if (startDate.HasValue)
            {
                query = query.Where(u => u.CreatedAt >= startDate.Value.Date);
            }
            if (endDate.HasValue)
            {
                query = query.Where(u => u.CreatedAt <= endDate.Value.Date.AddDays(1).AddTicks(-1));
            }

            query = query.OrderByDescending(u => u.CreatedAt);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<IEnumerable<User>> GetAllByRoleAsync(UserRole role)
        {
            return await _context.Users.AsNoTracking().Where(u => u.Role == role).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllAgentsAsync()
        {
            return await _context.Users.AsNoTracking().Where(u => u.Role == UserRole.SupportAgent).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user) => _context.Users.Update(user);

        public async Task<Dictionary<Guid, string>> GetUserNameDictionaryAsync(IEnumerable<Guid> userIds)
        {
            var distinctIds = userIds.Distinct().ToList();

            if (!distinctIds.Any())
            {
                return new Dictionary<Guid, string>();
            }

            return await _context.Users 
                .Where(u => distinctIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.Name); 
        }
    }
}