using System.ComponentModel.DataAnnotations;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Shared.DTOs.Users
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Nama lengkap wajib diisi.")]
        [StringLength(100, ErrorMessage = "Nama maksimal 100 karakter.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email wajib diisi.")]
        [EmailAddress(ErrorMessage = "Format email tidak valid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username wajib diisi.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username harus antara 3 hingga 50 karakter.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password wajib diisi.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password minimal 6 karakter.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Konfirmasi Password wajib diisi.")]
        [Compare(nameof(Password), ErrorMessage = "Password dan Konfirmasi Password tidak cocok.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role (Peran) wajib dipilih.")]
        public UserRole Role { get; set; }

        [Phone(ErrorMessage = "Format nomor telepon tidak valid.")]
        public string? PhoneNumber { get; set; }

        public DateTime? BirthDate { get; set; }

        [StringLength(100, ErrorMessage = "Jabatan maksimal 100 karakter.")]
        public string? JobTitle { get; set; }

        [StringLength(500, ErrorMessage = "Alamat maksimal 500 karakter.")]
        public string? Address { get; set; }
    }
}