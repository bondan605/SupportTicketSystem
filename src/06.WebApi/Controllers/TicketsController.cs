using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Tickets;

namespace SupportTicketSystem.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Requirement: Managers view all tickets
            return Ok(await _ticketService.GetAllTicketsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _ticketService.GetTicketByIdAsync(id));
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] string? status, [FromQuery] Guid? assignedTo)
        {
            // Special Requirement for Azwar: Manager Report with filters
            var result = await _ticketService.GetFilteredTicketsAsync(status, assignedTo);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
        {
            // Requirement: Agents create tickets
            var result = await _ticketService.CreateTicketAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketDto dto)
        {
            // Requirement: Agents update tickets
            await _ticketService.UpdateTicketAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/assign")]
        public async Task<IActionResult> Assign(Guid id, [FromBody] Guid userId)
        {
            // Requirement: Managers assign tickets
            await _ticketService.AssignTicketAsync(id, userId);
            return NoContent();
        }
    }
}