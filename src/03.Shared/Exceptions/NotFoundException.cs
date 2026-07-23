namespace SupportTicketSystem.Shared.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a requested domain entity or resource is not found.
    /// This exception is typically mapped to an HTTP 404 Not Found response by the global exception middleware.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class using the entity name and its unique identifier.
        /// </summary>
        /// <param name="name">The name of the entity that was not found (e.g., "Category").</param>
        /// <param name="key">The unique identifier (key) of the entity that was searched for.</param>
        /// <example>
        /// throw new NotFoundException(nameof(Category), id);
        /// </example>
        public NotFoundException(string name, object key)
            : base($"Entity '{name}' with id '{key}' was not found.")
        {
        }

        /// <summary>
        /// Creates a <see cref="NotFoundException"/> for the specified entity type, inferring the entity name from the generic type parameter.
        /// </summary>
        /// <typeparam name="TEntity">The domain entity type that was not found.</typeparam>
        /// <param name="key">The unique identifier (key) of the entity that was searched for.</param>
        /// <example>
        /// throw NotFoundException.For&lt;Transaction&gt;(id);
        /// </example>
        public static NotFoundException For<TEntity>(object key)
        {
            return new NotFoundException(typeof(TEntity).Name, key);
        }
    }
}