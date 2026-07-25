namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents the type of change/event recorded in a ticket's history.
    /// </summary>
    public enum TicketHistoryAction
    {
        /// <summary>
        /// The ticket was first created.
        /// </summary>
        TicketCreated = 0,

        /// <summary>
        /// The ticket's status was changed (e.g. Open to InProgress). OldValue/NewValue hold
        /// the previous and new status.
        /// </summary>
        StatusChanged = 1,

        /// <summary>
        /// The ticket's assignee was changed. OldValue/NewValue hold the previous and new
        /// assignee (e.g. "Unassigned" to a user's name).
        /// </summary>
        AssigneeChanged = 2,

        /// <summary>
        /// The ticket's priority was changed. OldValue/NewValue hold the previous and new
        /// priority.
        /// </summary>
        PriorityChanged = 3,

        /// <summary>
        /// A comment or note was added to the ticket. The comment content is stored in Note;
        /// OldValue/NewValue are not applicable.
        /// </summary>
        CommentAdded = 4,

        /// <summary>
        /// General ticket fields were edited (e.g. Title, Description, Category) without a
        /// specific status/assignee/priority change. OldValue/NewValue are typically not
        /// applicable; details may be summarized in Note if needed.
        /// </summary>
        TicketUpdated = 5,
    }
}