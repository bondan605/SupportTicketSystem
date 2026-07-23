using FluentValidation;
using SupportTicketSystem.Shared.DTOs.Tickets;

namespace SupportTicketSystem.Application.Validators
{
    public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketDtoValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.");

            RuleFor(x => x.CustomerEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Customer email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

            RuleFor(x => x.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Issue title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}