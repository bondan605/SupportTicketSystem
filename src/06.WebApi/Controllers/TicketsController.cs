using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Models;
using SupportTicketSystem.Shared.Extensions;

namespace SupportTicketSystem.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing ticket operations.
    /// </summary>
    [ApiController]
    [Route(ApiRoutes.Tickets.Base)]
    [Produces("application/json")]
    [Authorize]
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
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] PagedRequest request)
        {
            var userId = User.GetUserId();
            var role = User.GetRole();

            var data = role == "Manager"
                ? await _ticketService.GetAllTicketsAsync(request)
                : await _ticketService.GetTicketsForAgentAsync(userId, request);

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
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReport([FromQuery] string? status, [FromQuery] Guid? assignedTo, [FromQuery] PagedRequest request)
        {
            var result = await _ticketService.GetFilteredTicketsAsync(status, assignedTo, request);
            return Ok(ApiResponse<PagedResult<TicketDto>>.SuccessResponse(result, "Filtered tickets retrieved successfully."));
        }

        /// <summary>
        /// Retrieves the ticket list with search and classification filters.
        /// </summary>
        [HttpGet(ApiRoutes.Tickets.ListSegment)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTicketList([FromQuery] string? status, [FromQuery] Guid? assignedTo, [FromQuery] PagedRequest request, [FromQuery] string? priority, [FromQuery] string? category, [FromQuery] string? search)
        {
            var result = await _ticketService.GetTicketListAsync(status, assignedTo, request, priority, category, search);

            return Ok(ApiResponse<PagedResult<TicketDto>>.SuccessResponse(result, "Ticket list retrieved successfully."));
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
        [Authorize(Roles = "Manager, SupportAgent")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto dto)
        {
            var userId = User.GetUserId();
            var result = await _ticketService.CreateTicketAsync(dto, userId);
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
        [Authorize(Roles = "SupportAgent, Manager")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketDto dto)
        {
            var userId = User.GetUserId();
            var role = User.GetRole();
            await _ticketService.UpdateTicketAsync(id, dto, userId, role);
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
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.GetUserId();
            await _ticketService.DeleteTicketAsync(id, userId);
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
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Assign(Guid id, [FromBody] Guid userId)
        {
            var adminId = User.GetUserId();
            // Requirement: Managers assign tickets
            await _ticketService.AssignTicketAsync(id, userId, adminId);
            return Ok(ApiResponse.SuccessResponse(null, "Ticket assigned successfully."));
        }
    }
}