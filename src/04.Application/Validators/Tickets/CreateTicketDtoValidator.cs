using FluentValidation;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Tickets;

namespace SupportTicketSystem.Application.Validators
{
    /// <summary>
    /// Validates CreateTicketDto. This is a business-rules/data-shape gate, not a security
    /// boundary against SQL injection - that's already handled structurally by EF Core's
    /// parameterized queries (LINQ), as long as no raw SQL string concatenation is introduced
    /// elsewhere in the codebase. See chat for the full reasoning.
    /// </summary>
    public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(150).WithMessage("Customer name cannot exceed 150 characters.");

            RuleFor(x => x.CustomerEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Customer email is required.")
                .MaximumLength(254).WithMessage("Email cannot exceed 254 characters.") // RFC 5321 max
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.CustomerPhone)
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.")
                .Matches(@"^[0-9+\-\s()]{7,20}$").WithMessage("Phone number format is invalid.")
                .When(x => !string.IsNullOrWhiteSpace(x.CustomerPhone));

            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Issue title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(5000).WithMessage("Description cannot exceed 5000 characters.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid ticket type.");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid ticket category.");

            RuleFor(x => x.Impact)
                .IsInEnum().WithMessage("Invalid ticket impact.");

            RuleFor(x => x.Priority)
                .IsInEnum().WithMessage("Invalid ticket priority.");

            RuleFor(x => x.Application)
                .IsInEnum().WithMessage("Invalid application/system.");

            RuleFor(x => x.Application)
                .Equal(TicketApplication.None)
                .WithMessage("Application must be 'None' when Category is Hardware.")
                .When(x => x.Category == TicketCategory.Hardware);

            RuleFor(x => x.AssignedTo)
                .NotEqual(Guid.Empty).WithMessage("AssignedTo cannot be an empty Guid.")
                .When(x => x.AssignedTo.HasValue);

            RuleFor(x => x.EstimatedDueDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
                .WithMessage("Estimated due date cannot be in the past.")
                .When(x => x.EstimatedDueDate.HasValue);
        }
    }
}
