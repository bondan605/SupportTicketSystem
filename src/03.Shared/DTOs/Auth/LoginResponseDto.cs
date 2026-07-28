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

        /// <summary>UTC instant the token expires. Used by the host app to size the auth
        /// cookie's lifetime and to schedule the session-expiry warning.</summary>
        public DateTime ExpiresAt { get; set; }
    }
}
