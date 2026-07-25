namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents how urgently a ticket needs to be handled.
    /// </summary>
    public enum TicketPriority
    {
        /// <summary>
        /// Minor issue with little to no impact on daily work. Can be scheduled with no rush.
        /// </summary>
        Low = 0,

        /// <summary>
        /// Standard issue affecting normal work but has a workaround or is not urgent.
        /// </summary>
        Medium = 1,

        /// <summary>
        /// Critical issue significantly blocking work or affecting many users. Needs immediate attention.
        /// </summary>
        High = 2,
    }
}