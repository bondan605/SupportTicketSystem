namespace SupportTicketSystem.Domain.Enums
{
    /// <summary>
    /// Represents the internal company system/application that a ticket is related to.
    /// Used to route tickets to the appropriate team and to analyze which systems generate
    /// the most support tickets.
    /// </summary>
    public enum TicketApplication
    {
        /// <summary>
        /// Used when the ticket is not related to any specific application/system
        /// (e.g. Hardware category tickets, such as a broken laptop or keyboard).
        /// Planned improvement: when TicketCategory.Hardware is selected, this field should be
        /// automatically locked to None and the selection disabled in the form. This validation
        /// is not yet implemented; for now users select this value manually.
        /// </summary>
        None = 0,

        /// <summary>
        /// Customer Relationship Management system — for managing customer data, sales
        /// pipeline, and interactions.
        /// </summary>
        CRM = 1,

        /// <summary>
        /// Enterprise Resource Planning system — for finance, inventory, procurement, and
        /// core business operations.
        /// </summary>
        ERP = 2,

        /// <summary>
        /// Human Resource Information System — for employee data, attendance, payroll, and
        /// leave management.
        /// </summary>
        HRIS = 3,

        /// <summary>
        /// Corporate email and communication platform (e.g. Outlook, Gmail Workspace).
        /// </summary>
        Email = 4,

        /// <summary>
        /// Internal file storage and sharing system (e.g. shared drives, document management).
        /// </summary>
        FileServer = 5,

        /// <summary>
        /// The company's public-facing website or customer portal.
        /// </summary>
        Website = 6,

        /// <summary>
        /// Internal employee portal/intranet for announcements, requests, or self-service tools.
        /// </summary>
        InternalPortal = 7,

        /// <summary>
        /// Any system not covered by the categories above.
        /// </summary>
        Other = 8,
    }
}