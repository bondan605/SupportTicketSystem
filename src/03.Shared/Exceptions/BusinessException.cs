namespace SupportTicketSystem.Shared.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a business rule or domain logic is violated.
    /// This exception is typically mapped to an HTTP 400 Bad Request response by the global exception middleware.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the business rule violation.</param>
        /// <example>
        /// throw new BusinessException("An account with this name already exists.");
        /// </example>
        public BusinessException(string message) : base(message)
        {
        }
    }
}