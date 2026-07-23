namespace SupportTicketSystem.Shared.DTOs
{
    /// <summary>
    /// A standardized wrapper for all API responses to ensure consistency.
    /// </summary>
    /// <typeparam name="T">The type of the data being returned.</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; } = new();

        // Helper for success responses
        public static ApiResponse<T> SuccessResponse(T? data, string message = "Request processed successfully.")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        // Helper for error responses
        public static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }

    /// <summary>
    /// Non-generic version for responses that don't return data (e.g., Delete/Update).
    /// </summary>
    public class ApiResponse : ApiResponse<object> { }
}