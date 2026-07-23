using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.Tickets
{
    public class UpdateTicketDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
    }
}
