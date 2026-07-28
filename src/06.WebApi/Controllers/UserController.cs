using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Services;
using SupportTicketSystem.Domain.Enums;
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
        /// Retrieves all users, regardless of role.
        /// </summary>
        /// <returns>A collection of all users with their complete information.</returns>
        /// <response code="200">Returns the list of users successfully.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUser()
        {
            var data = await _userService.GetAllUserAsync();
            return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(data, "Users retrieved successfully."));
        }

        /// <summary>
        /// Retrieves all users filtered by a specific role.
        /// </summary>
        /// <param name="role">The role to filter users by (e.g. Manager, SupportAgent).</param>
        /// <returns>A collection of users that have the given role.</returns>
        /// <response code="200">Returns the filtered list of users successfully.</response>
        /// <response code="400">If the role segment is not a valid role value.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpGet("{role}")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUserByRole(UserRole role)
        {
            var data = await _userService.GetAllUserByRoleAsync(role);
            return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(data, "Users retrieved successfully."));
        }
    }
}

