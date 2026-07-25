using AutoMapper;
using FluentValidation;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Application.Common;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.TicketHistories;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Exceptions;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTicketDto> _createTicketValidator;
        private readonly IValidator<UpdateTicketDto> _updateTicketValidator;
        private readonly ITicketHistoryRepository _historyRepository;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateTicketDto> createTicketValidator, IValidator<UpdateTicketDto> updateTicketValidator, ITicketHistoryRepository historyRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createTicketValidator = createTicketValidator;
            _updateTicketValidator = updateTicketValidator;
            _historyRepository = historyRepository;
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid id)
        {
            var ticket = await GetAndValidateTicket(id);
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<PagedResult<TicketDto>> GetAllTicketsAsync(PagedRequest request)
        {
            var pagedTickets = await _unitOfWork.Tickets.GetAllAsync(request);

            return new PagedResult<TicketDto>
            {
                Items = _mapper.Map<IEnumerable<TicketDto>>(pagedTickets.Items),
                PageNumber = pagedTickets.PageNumber,
                PageSize = pagedTickets.PageSize,
                TotalCount = pagedTickets.TotalCount
            };
        }

        public async Task<PagedResult<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo, PagedRequest request)
        {
            var pagedTickets = await _unitOfWork.Tickets.GetFilteredTicketsAsync(status, assignedTo, request);
            return new PagedResult<TicketDto>
            {
                Items = _mapper.Map<IEnumerable<TicketDto>>(pagedTickets.Items),
                PageNumber = pagedTickets.PageNumber,
                PageSize = pagedTickets.PageSize,
                TotalCount = pagedTickets.TotalCount
            };
        }

        public async Task<PagedResult<TicketDto>> GetTicketsForAgentAsync(Guid userId, PagedRequest request)
        {
            var pagedTickets = await _unitOfWork.Tickets.GetTicketsForUserAsync(userId, request);

            return new PagedResult<TicketDto>
            {
                Items = _mapper.Map<IEnumerable<TicketDto>>(pagedTickets.Items),
                PageNumber = pagedTickets.PageNumber,
                PageSize = pagedTickets.PageSize,
                TotalCount = pagedTickets.TotalCount
            };
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto, Guid CreatedBy)
        {
            await _createTicketValidator.ValidateAndThrowAsync(dto);

            var ticket = _mapper.Map<Ticket>(dto);
            ticket.Id = Guid.NewGuid();
            ticket.Status = TicketStatus.Open;
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.CreatedBy = CreatedBy; 

            // Business Rule: TKT-00001 format
            int sequence = await _unitOfWork.Tickets.GetNextTicketSequenceAsync();
            ticket.TicketNumber = $"TKT-{sequence:D5}";

            await _unitOfWork.Tickets.AddAsync(ticket);
            await _historyRepository.AddAsync(new TicketHistory
            {
                TicketId = ticket.Id,
                Action = "Created",
                PreviousStatus = null,
                NewStatus = ticket.Status,
                CreatedBy = CurrentUser.UserId,
                ChangedBy = CurrentUser.UserId,
                Timestamp = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task UpdateTicketAsync(Guid id, UpdateTicketDto dto, Guid userId, string userRole)
        {
            await _updateTicketValidator.ValidateAndThrowAsync(dto);

            var ticket = await GetAndValidateTicket(id);

            // Business Rule: Closed tickets cannot be modified
            EnsureTicketNotClosed(ticket);
            var previousStatus = ticket.Status;

            if (userRole != "Manager" && ticket.CreatedBy != userId && ticket.AssignedTo != userId)
            {
                throw new UnauthorizedAccessException("You can only update tickets you created or are assigned to.");
            }

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.Status = dto.Status;
            ticket.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Tickets.Update(ticket);

            await _historyRepository.AddAsync(new TicketHistory
            {
                TicketId = ticket.Id,
                Action = "StatusChanged",
                PreviousStatus = previousStatus,
                NewStatus = ticket.Status,
                ChangedBy = CurrentUser.UserId,
                Timestamp = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(Guid id)
        {
            var ticket = await GetAndValidateTicket(id);
            EnsureTicketNotClosed(ticket);

            _unitOfWork.Tickets.Delete(ticket);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AssignTicketAsync(Guid ticketId, Guid userId)
        {
            var ticket = await GetAndValidateTicket(ticketId);
            EnsureTicketNotClosed(ticket);

            // Business Rule: Tickets can only be assigned to existing users
            bool userExists = await _unitOfWork.Users.ExistsAsync(userId);
            if (!userExists)
                throw new BusinessException("Target user does not exist.");

            ticket.AssignedTo = userId;
            ticket.Status = TicketStatus.InProgress;
            ticket.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Tickets.Update(ticket);
            await _historyRepository.AddAsync(new TicketHistory
            {
                TicketId = ticket.Id,
                Action = "Assigned",
                PreviousStatus = ticket.Status,
                NewStatus = ticket.Status,
                ChangedBy = CurrentUser.UserId,
                Timestamp = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();
        }

        // Helper Methods
        private async Task<Ticket> GetAndValidateTicket(Guid id)
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            if (ticket == null) throw new NotFoundException("Ticket not found.");
            return ticket;
        }

        private void EnsureTicketNotClosed(Ticket ticket)
        {
            if (ticket.Status == TicketStatus.Closed)
                throw new BusinessException("Closed tickets cannot be modified or deleted.");
        }

        public async Task<PagedResult<TicketHistoryDto>> GetTicketHistoriesAsync(PagedRequest request)
        {
            return await _historyRepository.GetAllAsync(request);
        }
    }
}