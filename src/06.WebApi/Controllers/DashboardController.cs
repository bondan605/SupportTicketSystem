using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Interfaces;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Dashboard;
using SupportTicketSystem.Shared.DTOs.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SupportTicketSystem.WebApi.Controllers
{
    /// <summary>
    /// Controller for managing dashboard operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(IDashboardService service) => _service = service;

        [HttpGet("summary")]
        public async Task<ActionResult<ApiResponse<DashboardSummaryDto>>> GetSummary()
        {
            var result = await _service.GetDashboardSummaryAsync();
            return Ok(ApiResponse<DashboardSummaryDto>.SuccessResponse(result, "Dashboard summary retrieved successfully."));
        }
    }
}