using System.ComponentModel.DataAnnotations;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.Users
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "Nama lengkap wajib diisi.")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role (Peran) wajib dipilih.")]
        public UserRole Role { get; set; }

        [Phone(ErrorMessage = "Format nomor telepon tidak valid.")]
        public string? PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(100, ErrorMessage = "Jabatan maksimal 100 karakter.")]
        public string? JobTitle { get; set; }

        [StringLength(500, ErrorMessage = "Alamat maksimal 500 karakter.")]
        public string? Address { get; set; }

        public bool Status { get; set; }
    }
}