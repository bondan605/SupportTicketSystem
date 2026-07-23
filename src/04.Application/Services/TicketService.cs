using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.Exceptions;

namespace SupportTicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TicketDto> GetTicketByIdAsync(Guid id)
        {
            var ticket = await GetAndValidateTicket(id);
            return MapToDto(ticket);
        }

        public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync();
            return tickets.Select(MapToDto);
        }

        public async Task<IEnumerable<TicketDto>> GetFilteredTicketsAsync(string? status, Guid? assignedTo)
        {
            // Special Requirement for Azwar: Manager Report filtering
            var tickets = await _unitOfWork.Tickets.GetFilteredTicketsAsync(status, assignedTo);
            return tickets.Select(MapToDto);
        }

        public async Task<TicketDto> CreateTicketAsync(CreateTicketDto dto)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                CustomerName = dto.CustomerName,
                CustomerEmail = dto.CustomerEmail,
                Title = dto.Title,
                Description = dto.Description,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            // Business Rule: TKT-00001 format
            int sequence = await _unitOfWork.Tickets.GetNextTicketSequenceAsync();
            ticket.TicketNumber = $"TKT-{sequence:D5}";

            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(ticket);
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

        private TicketDto MapToDto(Ticket ticket) => new TicketDto
        {
            Id = ticket.Id,
            TicketNumber = ticket.TicketNumber,
            CustomerName = ticket.CustomerName,
            CustomerEmail = ticket.CustomerEmail,
            Title = ticket.Title,
            Description = ticket.Description,
            Status = ticket.Status,
            AssignedTo = ticket.AssignedTo,
            CreatedAt = ticket.CreatedAt,
            UpdatedAt = ticket.UpdatedAt
        };
    }
}