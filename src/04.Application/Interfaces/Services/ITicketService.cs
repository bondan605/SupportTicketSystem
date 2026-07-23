using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Abstractions.Services
{
    public interface ITicketService
    {
        Task<TicketDto> GetTicketByIdAsync(Guid id);
        Task<PagedResult<TicketDto>> GetAllTicketsAsync(PagedRequest request);
        Task<PagedResult<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo, PagedRequest request);
        Task<TicketDto> CreateTicketAsync(CreateTicketDto dto);
        Task UpdateTicketAsync(Guid id, UpdateTicketDto dto);
        Task DeleteTicketAsync(Guid id);
        Task AssignTicketAsync(Guid ticketId, Guid userId);
    }
}