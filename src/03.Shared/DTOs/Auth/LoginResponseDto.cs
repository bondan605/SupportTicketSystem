using SupportTicketSystem.Domain.Enums;
using System.Text.Json.Serialization;

namespace SupportTicketSystem.Shared.DTOs.Auth
{
    public class LoginResponseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
