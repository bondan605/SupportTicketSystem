namespace SupportTicketSystem.Shared.DTOs.Reports.Requests;

/// <summary>
/// Optional overrides for demo data generation. Any field left null falls back to
/// ReportSeedOptions' default (matching the original hardcoded seeder behavior).
/// </summary>
public class ReportSeedRequestDto
{
    public int? DaysToGenerate { get; set; }
    public int? MinTicketsPerDay { get; set; }
    public int? MaxTicketsPerDay { get; set; }

    /// <summary>If set, overrides Min/MaxTicketsPerDay entirely with an exact total.</summary>
    public int? TotalTickets { get; set; }
}