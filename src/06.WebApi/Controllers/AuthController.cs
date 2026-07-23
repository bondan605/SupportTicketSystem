using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Auth;

namespace SupportTicketSystem.WebApi.Controllers
{
    [ApiController]
    [Route(ApiRoutes.Auth.Base)]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user and returns a session token with role information.
        /// </summary>
        /// <param name="dto">Login credentials (Email and Password).</param>
        /// <returns>Standardized response containing user details and token.</returns>
        /// <response code="200">Login successful.</response>
        /// <response code="400">Invalid request data.</response>
        /// <response code="500">If an unexpected internal server error occurs.</response>
        [HttpPost(ApiRoutes.Auth.LoginSegment)]
        [ProducesResponseType(typeof(ApiResponse<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(result, "Login successful. Welcome back!"));
        }
    }
}