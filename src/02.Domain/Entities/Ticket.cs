using SupportTicketSystem.Base.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public string TicketNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public Guid? AssignedTo { get; set; }

        // Navigation property
        public virtual User? Assignee { get; set; }
    }
}
