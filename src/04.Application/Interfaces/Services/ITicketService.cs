using SupportTicketSystem.Shared.DTOs.Tickets;

namespace SupportTicketSystem.Application.Abstractions.Services
{
    public interface ITicketService
    {
        Task<TicketDto> GetTicketByIdAsync(Guid id);
        Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
        Task<IEnumerable<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo);
        Task<TicketDto> CreateTicketAsync(CreateTicketDto dto);
        Task UpdateTicketAsync(Guid id, UpdateTicketDto dto);
        Task DeleteTicketAsync(Guid id);
        Task AssignTicketAsync(Guid ticketId, Guid userId);
    }
}