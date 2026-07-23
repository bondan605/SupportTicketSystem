using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;
using System.Linq;

namespace SupportTicketSystem.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing ticket operations.
    /// </summary>
    //[Authorize]
    [ApiController]
    [Route(ApiRoutes.Tickets.Base)]
    [Produces("application/json")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Retrieves all tickets.
        /// </summary>
        /// <returns>A list of all tickets.</returns>
        /// <response code="200">Returns the list of tickets successfully.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        //[Authorize(Roles = "Manager")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] PagedRequest request)
        {
            var data = await _ticketService.GetAllTicketsAsync(request);
            return Ok(ApiResponse<PagedResult<TicketDto>>.SuccessResponse(data, "Tickets retrieved successfully."));
        }

        /// <summary>
        /// Retrieves a ticket by its ID.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <returns>The ticket details.</returns>
        /// <response code="200">Returns the ticket successfully.</response>
        /// <response code="404">If the ticket is not found.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _ticketService.GetTicketByIdAsync(id);
            return Ok(ApiResponse<TicketDto>.SuccessResponse(data, "Ticket retrieved successfully."));
        }

        /// <summary>
        /// Retrieves a filtered report of tickets based on status and assigned user.
        /// </summary>
        /// <param name="status">The ticket status filter (optional).</param>
        /// <param name="assignedTo">The user ID to filter tickets by (optional).</param>
        /// <param name="request">Paging parameters for the result set.</param>
        /// <returns>A list of filtered tickets.</returns>
        /// <response code="200">Returns the filtered tickets successfully.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpGet(ApiRoutes.Tickets.ReportSegment)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReport([FromQuery] string? status, [FromQuery] Guid? assignedTo, [FromQuery] PagedRequest request)
        {
            var result = await _ticketService.GetFilteredTicketsAsync(status, assignedTo, request);
            return Ok(ApiResponse<PagedResult<TicketDto>>.SuccessResponse(result, "Filtered tickets retrieved successfully."));
        }

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="dto">The ticket creation data.</param>
        /// <returns>The newly created ticket.</returns>
        /// <response code="201">Returns the created ticket successfully.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
        {
            // Requirement: Agents create tickets
            var result = await _ticketService.CreateTicketAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, ApiResponse<object>.SuccessResponse(result, "Ticket created successfully."));
        }

        /// <summary>
        /// Updates an existing ticket.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <param name="dto">The ticket update data.</param>
        /// <response code="204">The ticket was updated successfully.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="404">If the ticket is not found.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketDto dto)
        {
            // Requirement: Agents update tickets
            await _ticketService.UpdateTicketAsync(id, dto);
            return Ok(ApiResponse.SuccessResponse(null, "Ticket updated successfully."));
        }

        /// <summary>
        /// Deletes a ticket.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <response code="204">The ticket was deleted successfully.</response>
        /// <response code="404">If the ticket is not found.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return Ok(ApiResponse.SuccessResponse(null, "Ticket deleted successfully."));
        }

        /// <summary>
        /// Assigns a ticket to a user.
        /// </summary>
        /// <param name="id">The ticket ID.</param>
        /// <param name="userId">The user ID to assign the ticket to.</param>
        /// <response code="204">The ticket was assigned successfully.</response>
        /// <response code="400">If the request data is invalid.</response>
        /// <response code="404">If the ticket or user is not found.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpPut(ApiRoutes.Tickets.AssignSegment)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Assign(Guid id, [FromBody] Guid userId)
        {
            // Requirement: Managers assign tickets
            await _ticketService.AssignTicketAsync(id, userId);
            return Ok(ApiResponse.SuccessResponse(null, "Ticket assigned successfully."));
        }
    }
}