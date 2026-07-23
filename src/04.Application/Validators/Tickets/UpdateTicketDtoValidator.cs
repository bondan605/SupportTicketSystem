using FluentValidation;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Tickets;

public class UpdateTicketDtoValidator : AbstractValidator<UpdateTicketDto>
{
    public UpdateTicketDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Status)
            .NotEmpty()
            .IsInEnum()
            .WithMessage("Invalid ticket status.");
    }
}