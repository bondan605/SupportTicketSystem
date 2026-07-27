namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 4: Ticket count per assignee (top N), including an "Unassigned" bucket
/// for tickets where AssignedTo is null.
/// </summary>
public class TicketsPerAssigneeDto
{
    public Guid? AssigneeId { get; set; }

    /// <summary>Display name of the assignee, or "Unassigned" when AssigneeId is null.</summary>
    public string AssigneeName { get; set; } = string.Empty;
    public int Count { get; set; }
}