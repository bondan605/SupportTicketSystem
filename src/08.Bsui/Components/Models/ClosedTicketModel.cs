namespace SupportTicketSystem.Bsui.Components.Models
{
    public class ClosedTicketModel
    {
        public string TicketNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime ClosedAt { get; set; }
        public string ClosedBy { get; set; } = string.Empty;
    }
}
