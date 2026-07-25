using SupportTicketSystem.Base.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Domain.Entities
{
    /// <summary>
    /// Represents a support ticket raised by a customer, tracking its details, status, and assignment throughout its lifecycle.
    /// </summary>
    public class Ticket : BaseEntity
    {
        /// <summary>
        /// The human-readable ticket number shown to users. Must follow the "TKT-XXXXX" format
        /// (e.g. "TKT-00001"), as required by the case study specification.
        /// </summary>
        public string TicketNumber { get; set; } = string.Empty;

        /// <summary>
        /// The name of the customer who reported the ticket.
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the customer who reported the ticket.
        /// </summary>
        public string CustomerEmail { get; set; } = string.Empty;

        /// <summary>
        /// The phone number of the customer who reported the ticket.
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// A short summary/title of the ticket.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The full description of the issue or request.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The current state of the ticket (e.g. Open, InProgress, Resolved, Closed).
        /// </summary>
        public TicketStatus Status { get; set; }

        /// <summary>
        /// How urgently the ticket needs to be handled.
        /// </summary>
        public TicketPriority Priority { get; set; }

        /// <summary>
        /// The nature of the ticket (e.g. Incident, ServiceRequest, Problem, ChangeRequest).
        /// </summary>
        public TicketType Type { get; set; }

        /// <summary>
        /// The functional area the ticket belongs to (e.g. Application, Access, Report, Hardware).
        /// </summary>
        public TicketCategory Category { get; set; }

        /// <summary>
        /// How widely the issue affects users or the organization.
        /// </summary>
        public TicketImpact Impact { get; set; }

        /// <summary>
        /// The internal company system/application the ticket is related to.
        /// Should be set to ApplicationSystem.None when Category is Hardware, since hardware
        /// issues are not tied to a specific application. This is not yet enforced automatically;
        /// see ApplicationSystem.None documentation for details on the planned validation.
        /// </summary>
        public TicketApplication Application { get; set; }

        /// <summary>
        /// The Id of the user this ticket is assigned to. Null if unassigned.
        /// </summary>
        public Guid? AssignedTo { get; set; }

        /// <summary>
        /// The estimated date by which the ticket should be resolved. Optional, set by the
        /// reporter or agent as a target/urgency indicator.
        /// </summary>
        public DateTime? EstimatedDueDate { get; set; }

        /// <summary>
        /// The timestamp when the ticket was closed. Null while the ticket is still open,
        /// in progress, or resolved but not yet closed.
        /// </summary>
        public DateTime? ClosedAt { get; set; }

        /// <summary>
        /// The user this ticket is assigned to. Navigation property for <see cref="AssignedTo"/>.
        /// One user can have many assigned tickets (one-to-many), matched with
        /// <see cref="User.AssignedTickets"/>.
        /// </summary>
        public virtual User? Assignee { get; set; }

        /// <summary>
        /// The history of changes made to this ticket (status changes, assignee changes,
        /// priority changes, comments, etc.). One ticket can have many history entries
        /// (one-to-many relationship).
        /// </summary>
        public virtual ICollection<TicketHistory> Histories { get; set; } = new List<TicketHistory>();
    }
}