namespace SupportTicketSystem.Application.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string>? Errors { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Timestamp = DateTime.UtcNow,
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Timestamp = DateTime.UtcNow,
                Success = false,
                Message = message,
                Data = default,
                Errors = errors
            };
        }
    }
}
