namespace SupportTicketSystem.Base.Entities
{
    /// <summary>
    /// Base class providing common audit fields for all entities in the system.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The timestamp when the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// The Id of the user who created this entity. Null if created by the system.
        /// </summary>
        public Guid? CreatedBy { get; set; }

        /// <summary>
        /// The timestamp of the most recent update to this entity. Null if never updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The Id of the user who last updated this entity. Null if never updated or updated
        /// by the system.
        /// </summary>
        public Guid? UpdatedBy { get; set; }
    }
}