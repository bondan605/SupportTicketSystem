using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.TicketHistories;

public class TicketHistoryDto
{
    public Guid Id { get; set; }

    public string TicketNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public TicketStatus? PreviousStatus { get; set; }

    public TicketStatus? NewStatus { get; set; }

    public Guid ChangedBy { get; set; }

    public string ChangedByName { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }
}