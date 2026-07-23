using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Services;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing user operations.
    /// </summary>
    [ApiController]
    [Route(ApiRoutes.Users.Base)]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the UsersController with the required services.
        /// </summary>
        /// <param name="userService">The service for user operations.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves the list of all available support agents.
        /// </summary>
        /// <returns>A collection of support agents with their complete information.</returns>
        /// <response code="200">Successfully retrieved the list of support agents.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgents()
        {
            var data = await _userService.GetAllAgentsAsync();

            return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(data, "Agents retrieved successfully."));
        }
    }
}

