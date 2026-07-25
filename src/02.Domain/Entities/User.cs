using SupportTicketSystem.Base.Entities;
using SupportTicketSystem.Domain.Enums;

namespace SupportTicketSystem.Domain.Entities
{
    /// <summary>
    /// Represents a user of the system, such as a support agent, manager, or administrator.
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// The user's full name (e.g. "Andi Pratama").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The unique login name used to sign in, separate from the full name (e.g. "admin").
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The user's email address. Used for login and notifications.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The hashed password used for authentication. Never store plain text passwords.
        /// </summary>
        public required string PasswordHash { get; set; }

        /// <summary>
        /// The user's access level within the system (e.g. SupportAgent, Manager).
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// The user's contact phone number.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// The user's date of birth.
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// The user's job title within the organization (e.g. "System Administrator").
        /// </summary>
        public string? JobTitle { get; set; }

        /// <summary>
        /// The user's physical/mailing address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Indicates whether the user's account is active. Inactive users should not be able
        /// to log in or be assigned new tickets.
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// The URL of the user's profile picture. Optional.
        /// </summary>
        public string? AvatarUrl { get; set; }

        /// <summary>
        /// The timestamp of the user's most recent successful login.
        /// </summary>
        public DateTime? LastLoginAt { get; set; }

        /// <summary>
        /// The collection of tickets currently assigned to this user for handling.
        /// One user can have many assigned tickets (one-to-many relationship), configured via
        /// Ticket.AssignedToId.
        /// </summary>
        public virtual ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    }
}