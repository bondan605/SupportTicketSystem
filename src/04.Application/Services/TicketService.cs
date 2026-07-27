using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.Exceptions;
using AutoMapper;
using SupportTicketSystem.Shared.Models;
using FluentValidation;

namespace SupportTicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTicketDto> _createTicketValidator;
        private readonly IValidator<UpdateTicketDto> _updateTicketValidator;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateTicketDto> createTicketValidator, IValidator<UpdateTicketDto> updateTicketValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createTicketValidator = createTicketValidator;
            _updateTicketValidator = updateTicketValidator;
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
            int sequence = await _unitOfWork.Tickets.GetNextTicketSequenceAsync();
            ticket.TicketNumber = $"TKT-{sequence:D5}";

            await _unitOfWork.Tickets.AddAsync(ticket);

            // LOGIC TICKET HISTORY: Sesuai format permintaan
            var history = new TicketHistory
            {

                TicketId = ticket.Id,

                Action = TicketHistoryAction.TicketCreated,

                ChangedBy = CreatedBy,

                Timestamp = DateTime.UtcNow

            };

            await _unitOfWork.TicketHistories.AddAsync(history);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task UpdateTicketAsync(Guid id, UpdateTicketDto dto, Guid userId, string userRole)
        {
            await _updateTicketValidator.ValidateAndThrowAsync(dto);

            var ticket = await GetAndValidateTicket(id);
            EnsureTicketNotClosed(ticket);

            if (userRole != "Manager" && ticket.CreatedBy != userId && ticket.AssignedTo != userId)
            {
                throw new UnauthorizedAccessException("You can only update tickets you created or are assigned to.");
            }

            // LOGIC TICKET HISTORY: Pengecekan Perubahan Status
            if (ticket.Status != dto.Status)
            {
                var history = new TicketHistory
                {
                    TicketId = ticket.Id,
                    Action = TicketHistoryAction.StatusChanged,
                    OldValue = ticket.Status.ToString(),
                    NewValue = dto.Status.ToString(),
                    ChangedBy = userId,
                    Timestamp = DateTime.UtcNow
                };
                await _unitOfWork.TicketHistories.AddAsync(history);
            }

            // (Opsional) Anda bisa tambahkan pengecekan spesifik lainnya 
            // seperti judul/deskripsi jika berubah, buat riwayat "TicketUpdated".

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.Status = dto.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Tickets.Update(ticket);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AssignTicketAsync(Guid ticketId, Guid userId, Guid assignedByAdminId)
        {
            var ticket = await GetAndValidateTicket(ticketId);
            EnsureTicketNotClosed(ticket);

            bool userExists = await _unitOfWork.Users.ExistsAsync(userId);
            if (!userExists)
                throw new BusinessException("Target user does not exist.");

            var oldAssigneeId = ticket.AssignedTo;
            ticket.AssignedTo = userId;
            ticket.Status = TicketStatus.InProgress;
            ticket.UpdatedAt = DateTime.UtcNow;

            // LOGIC TICKET HISTORY: Perubahan Assignee
            var history = new TicketHistory
            {
                TicketId = ticket.Id,
                Action = TicketHistoryAction.AssigneeChanged,
                OldValue = oldAssigneeId?.ToString() ?? "Unassigned",
                NewValue = userId.ToString(),
                ChangedBy = assignedByAdminId,
                Timestamp = DateTime.UtcNow
            };
            await _unitOfWork.TicketHistories.AddAsync(history);

            _unitOfWork.Tickets.Update(ticket);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(Guid id, Guid deletedByUserId)
        {
            var ticket = await GetAndValidateTicket(id);
            EnsureTicketNotClosed(ticket);
            _unitOfWork.Tickets.Delete(ticket);
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
    }
}