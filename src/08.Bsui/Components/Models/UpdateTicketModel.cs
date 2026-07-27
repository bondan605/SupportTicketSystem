using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Bsui.Components.Models
{
    /// <summary>
    /// Form/view model for the Update Ticket page.
    /// </summary>
    public class UpdateTicketModel
    {
        public string TicketNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string CreatedByDisplay { get; set; } = string.Empty;

        public TicketType TicketType { get; set; }
        public TicketCategory? Category { get; set; }
        public TicketImpact? Impact { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketApplication? Application { get; set; }

        public TicketStatus Status { get; set; }
        public TicketStatus PreviousStatus { get; set; }
        public DateTime PreviousStatusChangedAt { get; set; }
        public string StatusChangeNote { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? EstimatedDueDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;

        public UserDto? AssignedAgent { get; set; }
        public UserDto? CcAgent { get; set; }
    }
}
