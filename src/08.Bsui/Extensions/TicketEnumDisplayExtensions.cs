using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Bsui.Extensions
{
    /// <summary>
    /// Human-readable labels for the ticket domain enums. Keeps display text (including
    /// Indonesian wording for the placeholder-like "None" values) out of the Razor markup.
    /// </summary>
    public static class TicketEnumDisplayExtensions
    {
        public static string ToDisplayText(this TicketType value) => value switch
        {
            TicketType.Incident => "Incident",
            TicketType.ServiceRequest => "Service Request",
            TicketType.Problem => "Problem",
            TicketType.ChangeRequest => "Change Request",
            _ => value.ToString()
        };

        public static string ToDisplayText(this TicketStatus value) => value switch
        {
            TicketStatus.Open => "Open",
            TicketStatus.InProgress => "In Progress",
            TicketStatus.Resolved => "Resolved",
            TicketStatus.Closed => "Closed",
            _ => value.ToString()
        };

        public static string ToDisplayText(this TicketCategory value) => value switch
        {
            TicketCategory.Application => "Application",
            TicketCategory.Access => "Access",
            TicketCategory.Report => "Report",
            TicketCategory.Hardware => "Hardware",
            TicketCategory.Other => "Lainnya",
            _ => value.ToString()
        };

        public static string ToDisplayText(this TicketImpact value) => value switch
        {
            TicketImpact.SingleUser => "Satu Pengguna",
            TicketImpact.SomeUsers => "Beberapa Pengguna",
            TicketImpact.AllUsers => "Seluruh Pengguna",
            _ => value.ToString()
        };

        public static string ToDisplayText(this TicketPriority value) => value switch
        {
            TicketPriority.Low => "Low",
            TicketPriority.Medium => "Medium",
            TicketPriority.High => "High",
            _ => value.ToString()
        };

        public static string ToDisplayText(this TicketApplication value) => value switch
        {
            TicketApplication.None => "Tidak Terkait Aplikasi Tertentu",
            TicketApplication.CRM => "CRM",
            TicketApplication.ERP => "ERP",
            TicketApplication.HRIS => "HRIS",
            TicketApplication.Email => "Email Corporate",
            TicketApplication.FileServer => "File Server",
            TicketApplication.Website => "Website",
            TicketApplication.InternalPortal => "Internal Portal",
            TicketApplication.Other => "Lainnya",
            _ => value.ToString()
        };
    }
}
