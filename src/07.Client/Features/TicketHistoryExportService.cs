using Microsoft.JSInterop;
using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.DTOs.Tickets;
using System.Text;

namespace SupportTicketSystem.Client.Features
{
    public class TicketHistoryExportService : ITicketHistoryExportService
    {
        private readonly IJSRuntime _jsRuntime;

        // IJSRuntime di-inject ke constructor dengan aman
        public TicketHistoryExportService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task ExportToCsvAsync(IEnumerable<TicketHistoryDto> histories)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("ID History,Ticket Number,Action,Detail Perubahan,Changed By,Timestamp");

            foreach (var item in histories)
            {
                var detail = !string.IsNullOrEmpty(item.OldValue)
                    ? $"Dari {item.OldValue} ke {item.NewValue}"
                    : (item.Note ?? "-");

                // Hapus kutip ganda di dalam string agar format CSV tidak rusak
                detail = detail.Replace("\"", "\"\"");

                csvBuilder.AppendLine($"\"{item.Id}\",\"{item.TicketNumber}\",\"{item.Action}\",\"{detail}\",\"{item.ChangedByName ?? "System User"}\",\"{item.Timestamp.ToLocalTime():yyyy-MM-dd HH:mm}\"");
            }

            var fileBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var fileName = $"TicketHistories_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            // Eksekusi download langsung di browser
            await _jsRuntime.InvokeVoidAsync("eval", $@"
                (function() {{
                    const bytes = new Uint8Array([{string.Join(",", fileBytes)}]);
                    const blob = new Blob([bytes], {{ type: 'text/csv;charset=utf-8;' }});
                    const url = URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.href = url;
                    a.download = '{fileName}';
                    a.click();
                    URL.revokeObjectURL(url);
                }})();
            ");
        }

        public async Task ExportToPdfAsync(IEnumerable<TicketHistoryDto> histories)
        {
            var sb = new StringBuilder();
            // Desain PDF disesuaikan dengan Theme.txt Anda (Warna Emerald Green #059669)
            sb.Append(@"
                <html>
                <head>
                    <title>Laporan Ticket Histories</title>
                    <style>
                        body { font-family: Arial, sans-serif; padding: 24px; color: #111827; }
                        h2 { color: #059669; margin-bottom: 4px; }
                        p { color: #6B7280; font-size: 12px; margin-bottom: 16px; }
                        table { width: 100%; border-collapse: collapse; font-size: 11px; }
                        th, td { border: 1px solid #E5E7EB; padding: 8px 10px; text-align: left; }
                        th { background-color: #059669; color: white; }
                        tr:nth-child(even) { background-color: #F9FAFB; }
                    </style>
                </head>
                <body>
                    <h2>Laporan Riwayat Tiket</h2>
                    <p>Dicetak pada: " + DateTime.Now.ToString("dd MMM yyyy HH:mm") + @"</p>
                    <table>
                        <thead>
                            <tr>
                                <th>Ticket Number</th>
                                <th>Action</th>
                                <th>Detail Perubahan</th>
                                <th>Changed By</th>
                                <th>Timestamp</th>
                            </tr>
                        </thead>
                        <tbody>
            ");

            foreach (var h in histories)
            {
                var detail = !string.IsNullOrEmpty(h.OldValue) ? $"Dari {h.OldValue} ke {h.NewValue}" : (h.Note ?? "-");
                sb.Append($@"
                    <tr>
                        <td>{h.TicketNumber}</td>
                        <td>{h.Action}</td>
                        <td>{detail}</td>
                        <td>{(h.ChangedByName ?? "System User")}</td>
                        <td>{h.Timestamp.ToLocalTime():dd MMM yyyy HH:mm}</td>
                    </tr>
                ");
            }

            sb.Append(@"
                        </tbody>
                    </table>
                </body>
                </html>
            ");

            // Buka jendela print PDF yang rapi
            await _jsRuntime.InvokeVoidAsync("eval", $@"
                (function() {{
                    let win = window.open('', '_blank');
                    win.document.write(`{sb.ToString().Replace("`", "\\`")}`);
                    win.document.close();
                    win.focus();
                    setTimeout(() => {{ win.print(); win.close(); }}, 500);
                }})();
            ");
        }
    }
}