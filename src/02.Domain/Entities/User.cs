using SupportTicketSystem.Base.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        // Navigation property for tickets assigned to this user
        public virtual ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    }
}
