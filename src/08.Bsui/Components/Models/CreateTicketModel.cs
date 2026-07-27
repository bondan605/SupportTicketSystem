using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Bsui.Components.Models
{
    /// <summary>
    /// Form/view model for the Create Ticket page.
    /// </summary>
    public class CreateTicketModel
    {
        public TicketType TicketType { get; set; } = TicketType.Incident;
        public TicketStatus Status { get; set; } = TicketStatus.Open;

        public TicketCategory? Category { get; set; }
        public TicketImpact? Impact { get; set; }
        public TicketApplication? Application { get; set; }

        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
        public string Title { get; set; } = string.Empty;
        public DateTime? EstimatedUrgency { get; set; }
        public string Description { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string RequesterEmail { get; set; } = string.Empty;
        public string RequesterPhone { get; set; } = string.Empty;
        public UserDto? AssignedAgent { get; set; }
        public UserDto? CcAgent { get; set; }
    }
}
