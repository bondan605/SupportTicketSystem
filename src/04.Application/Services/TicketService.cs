using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.Exceptions;
using AutoMapper;

namespace SupportTicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid id)
        {
            var ticket = await GetAndValidateTicket(id);
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task<IEnumerable<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo)
        {
            // Special Requirement for Azwar: Manager Report filtering
            var tickets = await _unitOfWork.Tickets.GetFilteredTicketsAsync(status, assignedTo);
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto)
        {
            var ticket = _mapper.Map<Ticket>(dto);
            ticket.Id = Guid.NewGuid();
            ticket.Status = TicketStatus.Open;
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.CreatedBy = Guid.NewGuid(); // Assuming the system generates a new GUID for the creator; replace with actual user ID if available

            // Business Rule: TKT-00001 format
            int sequence = await _unitOfWork.Tickets.GetNextTicketSequenceAsync();
            ticket.TicketNumber = $"TKT-{sequence:D5}";

            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task UpdateTicketAsync(Guid id, UpdateTicketDto dto)
        {
            var ticket = await GetAndValidateTicket(id);

            // Business Rule: Closed tickets cannot be modified
            EnsureTicketNotClosed(ticket);

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.Status = dto.Status;
            ticket.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Tickets.Update(ticket);
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
            ticket.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Tickets.Update(ticket);
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