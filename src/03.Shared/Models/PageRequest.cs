namespace SupportTicketSystem.Shared.Models
{
    /// <summary>
    /// Standard model for requesting paginated data.
    /// </summary>
    public class PagedRequest
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        /// <summary>
        /// The requested page number (starting from 1).
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// The number of items per page. Maximum 50.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}