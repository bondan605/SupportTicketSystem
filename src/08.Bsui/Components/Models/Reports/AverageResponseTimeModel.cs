namespace SupportTicketSystem.Bsui.Components.Models.Reports
{
    public sealed record AverageResponseTimeModel(
        TimeSpan Duration,
        decimal ChangePercentage,
        bool IsIncrease);
}
