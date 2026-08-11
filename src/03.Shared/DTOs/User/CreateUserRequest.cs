using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.Users
{
    public class CreateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
    }
}