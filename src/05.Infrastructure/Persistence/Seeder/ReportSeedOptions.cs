namespace SupportTicketSystem.Infrastructure.Persistence.Seeding;

/// <summary>
/// Configurable parameters for demo ticket generation. All properties have sensible
/// defaults matching the original hardcoded seeder behavior, so callers can override
/// only what they need.
/// </summary>
public class ReportSeedOptions
{
    public int DaysToGenerate { get; set; } = 45;
    public int MinTicketsPerDay { get; set; } = 2;
    public int MaxTicketsPerDay { get; set; } = 6;

    /// <summary>
    /// If set, generates exactly this many tickets distributed randomly across
    /// DaysToGenerate, ignoring MinTicketsPerDay/MaxTicketsPerDay entirely.
    /// If null, falls back to the per-day Min/Max random range.
    /// </summary>
    public int? TotalTickets { get; set; }

    /// <summary>
    /// Rough estimate of how many tickets this configuration will produce, for preview
    /// purposes before actually seeding. Exact when TotalTickets is set; an average
    /// estimate (not a guarantee) when using the per-day Min/Max range.
    /// </summary>
    public int EstimatedTicketCount()
    {
        if (TotalTickets.HasValue)
        {
            return TotalTickets.Value;
        }

        var averagePerDay = (MinTicketsPerDay + MaxTicketsPerDay) / 2.0;
        return (int)Math.Round((DaysToGenerate + 1) * averagePerDay);
    }
}