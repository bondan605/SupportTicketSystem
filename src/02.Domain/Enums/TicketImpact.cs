namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents how widely an issue affects users or the organization. This is distinct from
    /// Priority: Impact measures the scope/reach of the problem, while Priority measures how
    /// urgently it needs to be handled.
    /// </summary>
    public enum TicketImpact
    {
        /// <summary>
        /// Only one specific user is affected by the issue.
        /// </summary>
        SingleUser = 0,

        /// <summary>
        /// A subset of users (e.g. one team, one role, or one department) is affected.
        /// </summary>
        SomeUsers = 1,

        /// <summary>
        /// The entire user base or organization is affected. Typically the most severe impact level.
        /// </summary>
        AllUsers = 2,
    }
}