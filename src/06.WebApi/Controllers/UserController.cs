using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Services;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Users;
using SupportTicketSystem.Shared.Extensions;
using SupportTicketSystem.Shared.Models;
using System.Security.Claims;

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

        ///// <summary>
        ///// Retrieves all users, regardless of role.
        ///// </summary>
        ///// <returns>A collection of all users with their complete information.</returns>
        ///// <response code="200">Returns the list of users successfully.</response>
        ///// <response code="500">If an unexpected internal server error occurs.</response>
        //[HttpGet]
        //[ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetAllUser()
        //{
        //    var data = await _userService.GetAllUserAsync();
        //    return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(data, "Users retrieved successfully."));
        //}

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Manager")]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<UserResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers(
            [FromQuery] PagedRequest request,
            [FromQuery] string? searchString,
            [FromQuery] UserRole? role,
            [FromQuery] bool? status,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

                if (currentUserRole == UserRole.SupportAgent.ToString())
                {
                    return StatusCode(StatusCodes.Status403Forbidden, ApiResponse<object>.FailureResponse("Anda tidak memiliki akses."));
                }

                var pagedUsers = await _userService.GetAllUsersDetailAsync(
                    currentUserRole,
                    request,
                    searchString,
                    role,
                    status,
                    startDate,
                    endDate);

                return Ok(ApiResponse<PagedResult<UserResponseDto>>.SuccessResponse(pagedUsers, "Daftar pengguna berhasil diambil."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.FailureResponse(ex.Message));
            }
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

        /// <summary>
        /// Retrieves all users filtered by agent role.
        /// </summary>
        /// <returns>A collection of users that have the agent role.</returns>
        /// <response code="200">Returns the filtered list of users successfully.</response>
        /// <response code="400">If the role segment is not a valid role value.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpGet(ApiRoutes.Users.AgentSegment)]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAgents()
        {
            var data = await _userService.GetAllAgentsAsync();
            return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(data, "Agents retrieved successfully."));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(request);
                return StatusCode(StatusCodes.Status201Created,
                    ApiResponse<UserResponseDto>.SuccessResponse(createdUser, "User created successfully."));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse<object>.FailureResponse(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest request)
        {
            var currentUserRole = User.GetRole(); 
            var result = await _userService.UpdateUserAsync(id, request, currentUserRole);
            return Ok(ApiResponse<UserResponseDto>.SuccessResponse(result, "Pengguna berhasil diperbarui."));
        }

        /// <summary>
        /// Retrieves details for a specific user.
        /// </summary>
        /// <param name="id">The specific user ID.</param>
        [HttpGet("detail/{id:guid}")]
        [Authorize(Roles = "SuperAdmin,Manager")]
        [ProducesResponseType(typeof(ApiResponse<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDetailUser(Guid id)
        {
            try
            {
                var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

                var userDetail = await _userService.GetUserDetailByIdAsync(id, currentUserRole);
                if (userDetail == null)
                {
                    return NotFound(ApiResponse<object>.FailureResponse("User tidak ditemukan."));
                }

                return Ok(ApiResponse<UserResponseDto>.SuccessResponse(userDetail, "Detail user berhasil diambil."));
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, ApiResponse<object>.FailureResponse(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ApiResponse<object>.FailureResponse(ex.Message));
            }
        }
    }
}

