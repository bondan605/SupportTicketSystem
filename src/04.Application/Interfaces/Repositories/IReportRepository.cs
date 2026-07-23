using SupportTicketSystem.Domain.Entities;

namespace SupportTicketSystem.Domain.Interfaces;

public interface IReportRepository
{
    /// <summary>
    /// Mengembalikan IQueryable agar filter (Where) bisa ditambahkan secara
    /// dinamis di Application layer sebelum eksekusi query (deferred execution).
    /// </summary>
    IQueryable<Ticket> GetFilterableQuery();
}