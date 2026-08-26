using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.Users
{
    public class UserResponseDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? JobTitle { get; set; }
        public string? Address { get; set; }
        public bool Status { get; set; }          
        public DateTime TanggalDibuat { get; set; } 
    }
}