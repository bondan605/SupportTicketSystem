namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents the functional area a ticket belongs to. Used for routing tickets to the
    /// appropriate team and for reporting/statistics.
    /// </summary>
    public enum TicketCategory
    {
        /// <summary>
        /// Issues or requests related to how an application/software behaves — bugs, errors,
        /// slow performance, features not working.
        /// </summary>
        Application = 0,

        /// <summary>
        /// Issues or requests related to user permissions, login, account access, or
        /// authorization (e.g. "Request new access", "Can't log in").
        /// </summary>
        Access = 1,

        /// <summary>
        /// Issues or requests related to generating, viewing, or exporting reports/data
        /// (e.g. "Report not showing", "Need monthly report").
        /// </summary>
        Report = 2,

        /// <summary>
        /// Issues related to physical devices — laptop, monitor, keyboard, mouse, printer,
        /// or other hardware malfunction/damage.
        /// Note: when this category is selected, the ticket's Application field should be
        /// locked to ApplicationSystem.None, since hardware issues are not related to any
        /// specific application/system. This auto-lock validation is a planned improvement
        /// and is not yet enforced; for now users select the Application field manually.
        /// </summary>
        Hardware = 3,

        /// <summary>
        /// Anything that doesn't clearly fit the categories above.
        /// </summary>
        Other = 4,
    }
}