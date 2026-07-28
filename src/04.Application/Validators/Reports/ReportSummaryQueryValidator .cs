using FluentValidation;
using SupportTicketSystem.Shared.DTOs.Reports.Requests;

namespace SupportTicketSystem.Application.Validators.Reports;

/// <summary>Validates the raw query parameters for the report summary endpoint.</summary>
public class ReportSummaryQueryDtoValidator : AbstractValidator<ReportSummaryQueryDto>
{
    public ReportSummaryQueryDtoValidator()
    {
        RuleFor(q => q.StartDate)
            .LessThanOrEqualTo(q => q.EndDate)
            .WithMessage("startDate cannot be later than endDate.");

        RuleFor(q => q.EndDate.Date)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("endDate cannot be in the future.");
    }
}