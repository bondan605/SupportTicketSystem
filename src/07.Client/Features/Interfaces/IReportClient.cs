using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Reports.Responses;

namespace SupportTicketSystem.Client.Features.Interfaces;
public interface IReportClient
{
    /// <summary>
    /// Mengambil ringkasan laporan (Report Summary) dari API.
    /// </summary>
    /// <param name="startDate">Batas awal tanggal (Opsional)</param>
    /// <param name="endDate">Batas akhir tanggal (Opsional)</param>
    /// <returns>Objek ApiResponse yang berisi ReportSummaryDto</returns>
    Task<ApiResponse<ReportSummaryDto>> GetReportSummaryAsync(DateTime? startDate, DateTime? endDate);
}