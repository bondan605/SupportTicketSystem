namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents the nature of the ticket, following common IT service management (ITSM) terminology.
    /// </summary>
    public enum TicketType
    {
        /// <summary>
        /// An unplanned interruption or reduction in quality of a service. Something is broken
        /// and needs to be fixed (e.g. "Login failed", "Data not saved").
        /// </summary>
        Incident = 0,

        /// <summary>
        /// A formal request for something new to be provisioned or granted. Nothing is broken;
        /// the user is asking for access, information, or a standard action
        /// (e.g. "New access request", "Password reset").
        /// </summary>
        ServiceRequest = 1,

        /// <summary>
        /// The underlying root cause of one or more incidents. Used when a recurring or
        /// unresolved incident needs deeper investigation to prevent it from happening again.
        /// </summary>
        Problem = 2,

        /// <summary>
        /// A request to modify, upgrade, or configure an existing system/application
        /// (e.g. adding a new feature, changing a setting, deploying an update).
        /// </summary>
        ChangeRequest = 3,
    }
}