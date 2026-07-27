using SupportTicketSystem.Application.Interfaces;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Interfaces;

namespace SupportTicketSystem.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly ITicketRepository _ticketRepository;

    public ReportService(IReportRepository reportRepository, ITicketRepository ticketRepository)
    {
        _reportRepository = reportRepository;
        _ticketRepository = ticketRepository;
    }
    
}