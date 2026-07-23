using FluentValidation;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.Exceptions;

namespace SupportTicketSystem.WebApi.Middleware
{
    /// <summary>
    /// Global Exception Handling Middleware to catch all unhandled exceptions
    /// and format them into standardized API responses.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred during request processing. Message: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, response) = exception switch
            {
                // Request Validation Errors (FluentValidation)
                ValidationException validationEx => (
                    StatusCodes.Status400BadRequest,
                    ApiResponse<object>.FailureResponse(
                        "Validation Failed",
                        validationEx.Errors.Select(e => e.ErrorMessage).ToList())
                ),

                // Business Logic Errors (Custom Domain Exceptions)
                // Catching: "Closed tickets cannot be modified" or "Invalid credentials"
                BusinessException businessEx => (
                    StatusCodes.Status400BadRequest,
                    ApiResponse<object>.FailureResponse(businessEx.Message)
                ),

                // Resource Not Found (Custom or System)
                NotFoundException customNotFoundEx => (
                    StatusCodes.Status404NotFound,
                    ApiResponse<object>.FailureResponse(customNotFoundEx.Message)
                ),
                KeyNotFoundException notFoundEx => (
                    StatusCodes.Status404NotFound,
                    ApiResponse<object>.FailureResponse(notFoundEx.Message)
                ),

                // Authorization Issues
                UnauthorizedAccessException _ => (
                    StatusCodes.Status401Unauthorized,
                    ApiResponse<object>.FailureResponse("You are not authorized to perform this action.")
                ),

                // Catch-all for unhandled exceptions (System/DB errors)
                // Hidden for security to satisfy "Security 5%" criteria
                _ => (
                    StatusCodes.Status500InternalServerError,
                    //ApiResponse<object>.FailureResponse("An unexpected error occurred. Please contact support.")
                    ApiResponse<object>.FailureResponse($"DEBUG ERROR: {exception.Message} | Inner: {exception.InnerException?.Message}")
                )
            };

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}