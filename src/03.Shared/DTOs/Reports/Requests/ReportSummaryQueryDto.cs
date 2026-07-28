using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicketSystem.Shared.DTOs.Reports.Requests;

/// <summary>Raw query parameters for GET /api/reports/summary.</summary>
public record ReportSummaryQueryDto(DateTime StartDate, DateTime EndDate);