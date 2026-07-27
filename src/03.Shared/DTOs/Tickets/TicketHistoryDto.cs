using SupportTicketSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace SupportTicketSystem.Shared.DTOs.Tickets
{
    public class TicketHistoryDto
    {
        public Guid Id { get; set; }

        public Guid TicketId { get; set; }

        /// <summary>
        /// Diambil dari relasi Ticket. Berguna untuk langsung ditampilkan di tabel UI.
        /// </summary>
        public string? TicketNumber { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketHistoryAction Action { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public string? Note { get; set; }

        public Guid ChangedBy { get; set; }

        /// <summary>
        /// Diambil dari relasi User (ChangedByUser). Berguna untuk menampilkan nama user di tabel UI.
        /// </summary>
        public string? ChangedByName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}