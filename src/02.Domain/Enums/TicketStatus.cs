namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents the current state of a ticket throughout its lifecycle.
    /// </summary>
    public enum TicketStatus
    {
        /// <summary>
        /// The ticket has just been created and has not yet been picked up for work.
        /// </summary>
        Open = 1,

        /// <summary>
        /// The ticket is actively being worked on by an assigned agent.
        /// </summary>
        InProgress = 2,

        /// <summary>
        /// The issue has been fixed or the request has been fulfilled, pending confirmation
        /// from the reporter before it can be marked as Closed.
        /// </summary>
        Resolved = 3,

        /// <summary>
        /// The ticket is finished and confirmed. No further action is expected.
        /// </summary>
        Closed = 4
    }
}