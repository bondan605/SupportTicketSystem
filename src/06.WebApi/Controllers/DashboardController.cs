using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Application.Interfaces;
using SupportTicketSystem.Shared.DTOs.Dashboard;

namespace SupportTicketSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        public DashboardController(IDashboardService service) => _service = service;

        [HttpGet("summary")]
        public async Task<ActionResult<DashboardSummaryDto>> GetSummary()
        {
            var result = await _service.GetDashboardSummaryAsync();
            return Ok(result);
        }
    }
}