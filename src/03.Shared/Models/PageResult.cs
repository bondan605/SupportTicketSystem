namespace SupportTicketSystem.Shared.Models
{
    /// <summary>
    /// A generic wrapper for paginated responses containing data and navigation metadata.
    /// </summary>
    /// <typeparam name="T">The type of data being returned.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// The collection of items for the current page.
        /// </summary>
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        /// <summary>
        /// The current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of records available in the database.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// The total number of pages based on TotalCount and PageSize.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        /// <summary>
        /// Indicates if there is a previous page available.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Indicates if there is a next page available.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}