namespace SupportTicketSystem.Shared.DTOs.Reports.Responses;

/// <summary>
/// Component 8: Percentage of closed tickets that met their SLA target
/// (ClosedAt &lt;= EstimatedDueDate). Tickets without an EstimatedDueDate are excluded
/// from both the numerator and denominator, since there is no target to evaluate against.
/// </summary>
public class SlaComplianceDto
{
    public double CompliancePercentage { get; set; }
    public int EvaluatedTicketCount { get; set; } // tickets with EstimatedDueDate set
    public double? ChangePercent { get; set; }
}