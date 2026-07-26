using SupportTicketSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace SupportTicketSystem.Shared.DTOs.Tickets
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketPriority Priority { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketCategory Category { get; set; }
        public Guid? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
