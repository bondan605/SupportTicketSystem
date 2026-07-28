using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Reports.Responses;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Features;

public class ReportClient : IReportClient
{
    private readonly HttpClient _httpClient;

    public ReportClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<ReportSummaryDto>> GetReportSummaryAsync(DateTime? startDate, DateTime? endDate)
    {
        var queryParameters = new List<string>();

        if (startDate.HasValue)
        {
            queryParameters.Add($"startDate={startDate.Value.ToString("yyyy-MM-dd")}");
        }

        if (endDate.HasValue)
        {
            queryParameters.Add($"endDate={endDate.Value.ToString("yyyy-MM-dd")}");
        }

        var queryString = queryParameters.Count > 0
            ? "?" + string.Join("&", queryParameters)
            : string.Empty;

        var url = $"{ApiRoutes.Report.Reports}/summary{queryString}";

        var response = await _httpClient.GetFromJsonAsync<ApiResponse<ReportSummaryDto>>(url);

        return response ?? ApiResponse<ReportSummaryDto>.FailureResponse("Gagal memproses response dari server.");
    }
}