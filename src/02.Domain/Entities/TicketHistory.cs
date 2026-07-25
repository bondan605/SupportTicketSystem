using SupportTicketSystem.Base.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Domain.Entities;

/// <summary>
/// Represents a single change/event recorded in a ticket's history, such as a status change, assignee change, priority change, comment, or general update.
/// </summary>
public class TicketHistory : BaseEntity
{
    /// <summary>
    /// The Id of the ticket this history entry belongs to.
    /// </summary>
    public Guid TicketId { get; set; }

    /// <summary>
    /// The type of change this entry represents (e.g. "StatusChanged", "AssigneeChanged",
    /// "PriorityChanged", "CommentAdded", "TicketUpdated"). Used to determine how OldValue/
    /// NewValue/Note should be interpreted and displayed.
    /// </summary>
    public required string Action { get; set; }

    /// <summary>
    /// The value before the change (e.g. previous status, previous assignee name, previous
    /// priority). Its meaning depends on <see cref="Action"/>. Null when not applicable
    /// (e.g. for "CommentAdded").
    /// </summary>
    public string? OldValue { get; set; }

    /// <summary>
    /// The value after the change (e.g. new status, new assignee name, new priority).
    /// Its meaning depends on <see cref="Action"/>. Null when not applicable
    /// (e.g. for "CommentAdded").
    /// </summary>
    public string? NewValue { get; set; }

    /// <summary>
    /// Free-text note or comment content. Used for the "CommentAdded" action, or to provide
    /// additional context for other actions.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// The Id of the user who made this change.
    /// </summary>
    public Guid ChangedBy { get; set; }

    /// <summary>
    /// The exact date and time the change occurred.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The ticket this history entry belongs to. Navigation property for <see cref="TicketId"/>.
    /// </summary>
    public Ticket? Ticket { get; set; }

    /// <summary>
    /// The user who made this change. Navigation property for <see cref="ChangedBy"/>.
    /// </summary>
    public User? ChangedByUser { get; set; }
}