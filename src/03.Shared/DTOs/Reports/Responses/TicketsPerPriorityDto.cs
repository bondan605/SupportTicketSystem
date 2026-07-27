namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 6: One entry per TicketPriority value.
/// </summary>
public class TicketsPerPriorityDto
{
    public string Priority { get; set; } = string.Empty; // "Low", "Medium", "High"
    public int Count { get; set; }
    public double Percentage { get; set; }
}