using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Client.Features.Interfaces;

public interface ITicketClient
{
    Task<ApiResponse<TicketDto>> GetTicketByIdAsync(Guid id);
    Task<PagedResult<TicketDto>> GetAllTicketsAsync(PagedRequest request);
    Task<PagedResult<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo, PagedRequest request);
    Task<ApiResponse<object>> CreateTicketAsync(CreateTicketDto dto);
    Task<ApiResponse<object>> UpdateTicketAsync(Guid id, UpdateTicketDto dto);
    Task<ApiResponse<object>> DeleteTicketAsync(Guid id);
    Task<ApiResponse<object>> AssignTicketAsync(Guid ticketId, Guid userId);
}