using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.Tickets
{
    /// <summary>
    /// Payload for creating a ticket (POST /api/tickets).
    /// </summary>
    public class CreateTicketDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public TicketStatus Status { get; set; }
        public TicketType Type { get; set; }
        public TicketCategory Category { get; set; }
        public TicketImpact Impact { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketApplication Application { get; set; }

        /// <summary>Id of the user to assign the ticket to. Null leaves it unassigned.</summary>
        public Guid? AssignedTo { get; set; }

        /// <summary>Target resolution date, optionally set by the reporter/agent.</summary>
        public DateTime? EstimatedDueDate { get; set; }
    }
}