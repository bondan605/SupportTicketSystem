namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 7: Average time between a ticket's CreatedAt and the first TicketHistory
/// entry where Action == StatusChanged marking the transition into InProgress.
/// Tickets that never left Open status within the filter range are excluded.
/// </summary>
public class AverageResponseTimeDto
{
    public double AverageResponseMinutes { get; set; }

    /// <summary>Percentage change vs. the equivalent previous period.</summary>
    public double? ChangePercent { get; set; }
}