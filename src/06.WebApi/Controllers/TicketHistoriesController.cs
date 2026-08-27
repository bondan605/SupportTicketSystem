using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Application.Services;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.Extensions;
using SupportTicketSystem.Shared.Models;
using ClosedXML.Excel;

namespace SupportTicketSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/ticket-histories")]
    [Produces("application/json")]
    [Authorize]
    public class TicketHistoriesController : ControllerBase
    {
        private readonly ITicketHistoryService _historyService;
        private readonly IUserService _userService;

        public TicketHistoriesController(ITicketHistoryService historyService, IUserService userService)
        {
            _historyService = historyService;
            _userService = userService;
        }

        // Managers see every ticket's history; any other role only sees history for tickets
        // they created or are assigned to.
        private Guid? GetScopedToUserId()
        {
            var role = User.GetRole();
            return (role == "Manager" || role == "SuperAdmin") ? null : User.GetUserId();
        }

        /// <summary>Agent Id -> Name, used to resolve AssigneeChanged's raw Guid Old/NewValue
        /// into a readable name for CSV/PDF exports (the UI resolves this client-side; exports
        /// need their own lookup since they're generated entirely server-side).</summary>
        private async Task<Dictionary<Guid, string>> GetAgentNameLookupAsync()
        {
            var agents = await _userService.GetAllAgentsAsync();
            return agents.ToDictionary(a => a.Id, a => a.Name);
        }

        private static string ResolveHistoryValue(TicketHistoryAction action, string? value, IReadOnlyDictionary<Guid, string> agentNames)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "-";

            if (action == TicketHistoryAction.AssigneeChanged && Guid.TryParse(value, out var userId))
                return agentNames.TryGetValue(userId, out var name) ? name : "Unknown User";

            return value;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<TicketHistoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilteredHistories(
        [FromQuery] Guid? ticketId,
        [FromQuery] string? action,
        [FromQuery] Guid? changedBy,
        [FromQuery] string? search,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] PagedRequest request)
        {
            var result = await _historyService.GetFilteredHistoriesAsync(ticketId, action, changedBy, search, startDate, endDate, request, GetScopedToUserId());
            return Ok(ApiResponse<PagedResult<TicketHistoryDto>>.SuccessResponse(result, "Ticket histories retrieved successfully."));
        }

        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportExcel(
        [FromQuery] Guid? ticketId,
        [FromQuery] string? action,
        [FromQuery] Guid? changedBy,
        [FromQuery] string? search,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
        {
            var histories = await _historyService.GetAllForExportAsync(
                ticketId, action, changedBy, search, startDate, endDate, GetScopedToUserId()) ?? new List<TicketHistoryDto>();

            var agentNames = await GetAgentNameLookupAsync();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ticket Histories");

            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#303F9F");
            headerRow.Style.Font.FontColor = XLColor.White;

            worksheet.Cell(1, 1).Value = "History ID";
            worksheet.Cell(1, 2).Value = "Ticket Number";
            worksheet.Cell(1, 3).Value = "Action";
            worksheet.Cell(1, 4).Value = "Detail Perubahan";
            worksheet.Cell(1, 5).Value = "Changed By";
            worksheet.Cell(1, 6).Value = "Timestamp";

            int row = 2;
            int counter = 1;

            foreach (var item in histories)
            {
                string historyNumber = $"HS-{item.Timestamp.Year}-{counter:D5}";

                var detail = item.Action == TicketHistoryAction.TicketCreated
                    ? ($"Ticket created by {item.ChangedByName}" ?? "Ticket created")
                    : (!string.IsNullOrEmpty(item.OldValue)
                        ? $"Dari {ResolveHistoryValue(item.Action, item.OldValue, agentNames)} ke {ResolveHistoryValue(item.Action, item.NewValue, agentNames)}"
                        : (item.Note ?? "-"));

                worksheet.Cell(row, 1).Value = historyNumber;
                worksheet.Cell(row, 2).Value = item.TicketNumber ?? "-";
                worksheet.Cell(row, 3).Value = item.Action.ToString();
                worksheet.Cell(row, 4).Value = detail;
                worksheet.Cell(row, 5).Value = item.ChangedByName ?? "System User";
                worksheet.Cell(row, 6).Value = item.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm");

                row++;
                counter++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            var fileName = $"TicketHistories_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpGet("export-pdf")]
        public async Task<IActionResult> ExportPdf(
            [FromQuery] Guid? ticketId,
            [FromQuery] string? action,
            [FromQuery] Guid? changedBy,
            [FromQuery] string? search,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var histories = await _historyService.GetAllForExportAsync(
                ticketId, action, changedBy, search, startDate, endDate, GetScopedToUserId()) ?? new List<TicketHistoryDto>();

            var agentNames = await GetAgentNameLookupAsync();

            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(c => ComposeContent(c, histories, agentNames));
                    page.Footer()
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Halaman ");
                            text.CurrentPageNumber();
                            text.Span(" dari ");
                            text.TotalPages();
                        });
                });
            }).GeneratePdf();

            var fileName = $"TicketHistories_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text("Ticket Histories").FontSize(20).Bold().FontColor("#303F9F");
                    column.Item().Text($"Dicetak pada: {DateTime.Now:dd MMM yyyy HH:mm}").FontSize(8).FontColor(Colors.Grey.Medium);
                });
            });
        }

        private void ComposeContent(IContainer container, IEnumerable<TicketHistoryDto> histories, IReadOnlyDictionary<Guid, string> agentNames)
        {
            container.PaddingVertical(10).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Background("#303F9F").Padding(6).Text("Ticket Number").FontColor(Colors.White).Bold();
                    header.Cell().Background("#303F9F").Padding(6).Text("Action").FontColor(Colors.White).Bold();
                    header.Cell().Background("#303F9F").Padding(6).Text("Detail Perubahan").FontColor(Colors.White).Bold();
                    header.Cell().Background("#303F9F").Padding(6).Text("Changed By").FontColor(Colors.White).Bold();
                    header.Cell().Background("#303F9F").Padding(6).Text("Timestamp").FontColor(Colors.White).Bold();
                });

                bool alternate = false;
                foreach (var h in histories)
                {
                    var detail = h.Action == TicketHistoryAction.TicketCreated
                                ? ($"Ticket created by {h.ChangedByName}" ?? "Ticket created")
                                : (!string.IsNullOrEmpty(h.OldValue)
                                    ? $"Dari {ResolveHistoryValue(h.Action, h.OldValue, agentNames)} ke {ResolveHistoryValue(h.Action, h.NewValue, agentNames)}"
                                    : (h.Note ?? "-"));
                    var bg = alternate ? "#F9FAFB" : "#FFFFFF";

                    table.Cell().Background(bg).BorderBottom(1).BorderColor("#E5E7EB").Padding(6).Text(h.TicketNumber ?? "-");
                    table.Cell().Background(bg).BorderBottom(1).BorderColor("#E5E7EB").Padding(6).Text(h.Action.ToString());
                    table.Cell().Background(bg).BorderBottom(1).BorderColor("#E5E7EB").Padding(6).Text(detail);
                    table.Cell().Background(bg).BorderBottom(1).BorderColor("#E5E7EB").Padding(6).Text(h.ChangedByName ?? "System User");
                    table.Cell().Background(bg).BorderBottom(1).BorderColor("#E5E7EB").Padding(6).Text(h.Timestamp.ToLocalTime().ToString("dd MMM yyyy HH:mm"));

                    alternate = !alternate;
                }
            });
        }
    }
}