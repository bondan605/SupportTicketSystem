using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Infrastructure.Persistence;
using SupportTicketSystem.Infrastructure.Persistence.Seeding;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Reports.Requests;

namespace SupportTicketSystem.Api.Controllers;

[ApiController]
[Route("api/dev/report-data")]
public class DevDataController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public DevDataController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpDelete("clear")]
    public async Task<ActionResult<ApiResponse<object>>> ClearTicketData()
    {
        if (!_environment.IsDevelopment())
        {
            return Forbid();
        }

        await ReportDemoDataSeeder.ClearAsync(_context);

        return Ok(ApiResponse<object>.SuccessResponse(null, "Ticket and TicketHistory data cleared."));
    }

    /// <summary>
    /// Generates demo Ticket/TicketHistory data.
    /// </summary>
    [HttpPost("seed")]
    public async Task<ActionResult<ApiResponse<object>>> SeedTicketData([FromQuery] ReportSeedRequestDto request)
    {
        if (!_environment.IsDevelopment())
        {
            return Forbid();
        }

        var options = new ReportSeedOptions();
        if (request.DaysToGenerate.HasValue) options.DaysToGenerate = request.DaysToGenerate.Value;
        if (request.MinTicketsPerDay.HasValue) options.MinTicketsPerDay = request.MinTicketsPerDay.Value;
        if (request.MaxTicketsPerDay.HasValue) options.MaxTicketsPerDay = request.MaxTicketsPerDay.Value;
        if (request.TotalTickets.HasValue) options.TotalTickets = request.TotalTickets.Value;

        if (options.MinTicketsPerDay > options.MaxTicketsPerDay)
        {
            return BadRequest(ApiResponse<object>.FailureResponse(
                "minTicketsPerDay cannot be greater than maxTicketsPerDay."));
        }
        if (options.DaysToGenerate < 0 || (options.TotalTickets.HasValue && options.TotalTickets <= 0))
        {
            return BadRequest(ApiResponse<object>.FailureResponse(
                "daysToGenerate must be >= 0 and totalTickets, if provided, must be > 0."));
        }

        var estimatedCount = options.EstimatedTicketCount();
        var insertedCount = await ReportDemoDataSeeder.SeedAsync(_context, options);

        return Ok(ApiResponse<object>.SuccessResponse(
            new
            {
                RequestedOptions = options,
                EstimatedTicketCount = estimatedCount,
                ActualTicketsInserted = insertedCount
            },
            insertedCount == 0
                ? "Seed skipped — database already has enough tickets. Call /clear first to force a fresh reseed."
                : $"{insertedCount} demo tickets generated."));
    }

    [HttpPost("reset")]
    public async Task<ActionResult<ApiResponse<object>>> ResetAndSeedTicketData([FromQuery] ReportSeedRequestDto request)
    {
        if (!_environment.IsDevelopment())
        {
            return Forbid();
        }

        var options = new ReportSeedOptions();
        if (request.DaysToGenerate.HasValue) options.DaysToGenerate = request.DaysToGenerate.Value;
        if (request.MinTicketsPerDay.HasValue) options.MinTicketsPerDay = request.MinTicketsPerDay.Value;
        if (request.MaxTicketsPerDay.HasValue) options.MaxTicketsPerDay = request.MaxTicketsPerDay.Value;
        if (request.TotalTickets.HasValue) options.TotalTickets = request.TotalTickets.Value;

        await ReportDemoDataSeeder.ClearAsync(_context);
        var insertedCount = await ReportDemoDataSeeder.SeedAsync(_context, options);

        return Ok(ApiResponse<object>.SuccessResponse(
            new { RequestedOptions = options, ActualTicketsInserted = insertedCount },
            $"Database cleared and {insertedCount} demo tickets generated."));
    }
}