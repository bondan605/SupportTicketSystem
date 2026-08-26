using FluentValidation;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Application.Validators.Users
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama lengkap wajib diisi.")
                .MaximumLength(100).WithMessage("Nama maksimal 100 karakter.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email wajib diisi.")
                .EmailAddress().WithMessage("Format email tidak valid.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username wajib diisi.")
                .Length(3, 50).WithMessage("Username harus antara 3 hingga 50 karakter.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password wajib diisi.")
                .MinimumLength(6).WithMessage("Password minimal 6 karakter.")
                .MaximumLength(100).WithMessage("Password maksimal 100 karakter.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Konfirmasi Password wajib diisi.")
                .Equal(x => x.Password).WithMessage("Password dan Konfirmasi Password tidak cocok.");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Role (Peran) wajib dipilih dan valid.");

            RuleFor(x => x.PhoneNumber)
                // Memastikan hanya berisi angka, spasi, plus, atau strip jika diisi
                .Matches(@"^[0-9\+\-\s]+$").When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage("Format nomor telepon tidak valid.")
                .MaximumLength(20).WithMessage("Nomor telepon maksimal 20 karakter.");

            RuleFor(x => x.JobTitle)
                .MaximumLength(100).WithMessage("Jabatan maksimal 100 karakter.");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Alamat maksimal 500 karakter.");
        }
    }
}