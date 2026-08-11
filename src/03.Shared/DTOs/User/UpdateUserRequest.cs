using SupportTicketSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SupportTicketSystem.Shared.DTOs.Users
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "Nama wajib diisi")]
        public string Name { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public string? PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? JobTitle { get; set; }
        public string? Address { get; set; }

        public bool Status { get; set; }
    }
}