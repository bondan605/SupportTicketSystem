using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Infrastructure.Persistence;
using SupportTicketSystem.Infrastructure.Persistence.Seeding;
using SupportTicketSystem.Shared.DTOs;

namespace SupportTicketSystem.Api.Controllers;

/// <summary>
/// Development-only utility endpoints for managing demo report data.
/// </summary>
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

    /// <summary>Deletes all Ticket and TicketHistory rows.</summary>
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
    /// Generates ~45 days of demo Ticket/TicketHistory data. Skipped if the database already
    /// has enough tickets — call /clear first if you want a guaranteed fresh reseed.
    /// </summary>
    [HttpPost("seed")]
    public async Task<ActionResult<ApiResponse<object>>> SeedTicketData()
    {
        if (!_environment.IsDevelopment())
        {
            return Forbid();
        }

        var insertedCount = await ReportDemoDataSeeder.SeedAsync(_context);

        return Ok(ApiResponse<object>.SuccessResponse(
            new { TicketsInserted = insertedCount },
            insertedCount == 0
                ? "Seed skipped — database already has enough tickets. Call /clear first to force a fresh reseed."
                : $"{insertedCount} demo tickets generated."));
    }

    /// <summary>Convenience endpoint: Clear followed immediately by Seed, in one call.</summary>
    [HttpPost("reset")]
    public async Task<ActionResult<ApiResponse<object>>> ResetAndSeedTicketData()
    {
        if (!_environment.IsDevelopment())
        {
            return Forbid();
        }

        await ReportDemoDataSeeder.ClearAsync(_context);
        var insertedCount = await ReportDemoDataSeeder.SeedAsync(_context);

        return Ok(ApiResponse<object>.SuccessResponse(
            new { TicketsInserted = insertedCount },
            $"Database cleared and {insertedCount} demo tickets generated."));
    }
}