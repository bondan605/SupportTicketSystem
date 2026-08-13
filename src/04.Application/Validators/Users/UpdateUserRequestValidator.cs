using FluentValidation;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Application.Validators.Users
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nama lengkap wajib diisi.")
                .MaximumLength(100).WithMessage("Nama maksimal 100 karakter.");

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