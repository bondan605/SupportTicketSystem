namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 5 & 9: One entry per TicketCategory value. Shared shape for both the donut
/// chart (component 5) and the tabular breakdown (component 9), since they present the
/// same underlying data.
/// </summary>
public class TicketsPerCategoryDto
{
    public string Category { get; set; } = string.Empty; // e.g. "Application", "Hardware"
    public int Count { get; set; }
    public double Percentage { get; set; }
}