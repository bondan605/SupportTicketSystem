namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents the access level and responsibilities of a user within the system.
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Handles day-to-day tickets: creating, updating, and resolving tickets assigned to them.
        /// </summary>
        SupportAgent = 1,

        /// <summary>
        /// Oversees support operations: can view reports, manage users, and has broader
        /// access across all tickets in addition to SupportAgent capabilities.
        /// Assign ticket to an agent
        /// </summary>
        Manager = 2
    }
}