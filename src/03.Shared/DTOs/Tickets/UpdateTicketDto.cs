using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.Tickets
{
    /// <summary>
    /// Payload for updating a ticket (PUT /api/tickets/{id}).
    /// </summary>
    public class UpdateTicketDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketType Type { get; set; }
        public TicketCategory Category { get; set; }
        public TicketImpact Impact { get; set; }
        public TicketApplication Application { get; set; }
        public DateTime? EstimatedDueDate { get; set; }
        public string? StatusChangeNote { get; set; }

        /// <summary>Id of the user to assign the ticket to. Null leaves it unassigned.</summary>
        public Guid? AssignedTo { get; set; }
    }
}