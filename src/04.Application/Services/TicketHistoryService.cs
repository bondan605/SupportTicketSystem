using AutoMapper;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Services
{
    public class TicketHistoryService : ITicketHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<TicketHistoryDto>> GetFilteredHistoriesAsync(Guid? ticketId, string? action, Guid? changedBy,string? search, DateTime? startDate,
    DateTime? endDate, PagedRequest request)
        {
            var pagedHistories = await _unitOfWork.TicketHistories.GetFilteredHistoriesAsync(ticketId, action, changedBy,search, startDate, endDate, request);

            return new PagedResult<TicketHistoryDto>
            {
                Items = _mapper.Map<IEnumerable<TicketHistoryDto>>(pagedHistories.Items),
                PageNumber = pagedHistories.PageNumber,
                PageSize = pagedHistories.PageSize,
                TotalCount = pagedHistories.TotalCount
            };
        }
    }
}