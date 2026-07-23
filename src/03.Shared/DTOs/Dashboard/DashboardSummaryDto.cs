namespace SupportTicketSystem.Shared.DTOs.Dashboard;

public class DashboardSummaryDto
{
    // Statistik Utama sesuai Business Scenario (Visibility into workloads)
    public int TotalTickets { get; set; }
    public int OpenTickets { get; set; }
    public int InProgressTickets { get; set; }
    public int ResolvedTickets { get; set; }
    public int ClosedTickets { get; set; }

    // Penting bagi Manager untuk melihat tiket yang belum ditugaskan
    public int UnassignedTickets { get; set; }

    // Opsional: Untuk grafik sederhana di MudBlazor
    public List<TicketTrendDto> WeeklyTrends { get; set; } = new();
}

public class TicketTrendDto
{
    public string DayName { get; set; } = string.Empty;
    public int Count { get; set; }
}