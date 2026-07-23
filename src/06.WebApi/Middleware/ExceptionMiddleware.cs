using SupportTicketSystem.Application.Common.Responses;
using FluentValidation;
using SupportTicketSystem.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
                // Continue the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception centrally
                // We log it here so we don't have to duplicate _logger.LogError in every specific case
                _logger.LogError(ex, "An exception occurred during request processing. Message: {Message}", ex.Message);

                // Delegate to the unified exception handler
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the content type once
            context.Response.ContentType = "application/json";

            // Use modern C# switch expression to determine status code and response body
            var (statusCode, response) = exception switch
            {
                // Request Validation Errors (FluentValidation)
                ValidationException validationEx => (
                    StatusCodes.Status400BadRequest,
                    ApiResponse<object>.ErrorResponse(
                        "Validation Failed",
                        validationEx.Errors.Select(e => e.ErrorMessage).ToList())
                ),

                // Business Logic Errors (Custom Domain Exceptions)
                BusinessException businessEx => (
                    StatusCodes.Status400BadRequest,
                    ApiResponse<object>.ErrorResponse(businessEx.Message)
                ),

                // Custom Domain Resource Not Found
                NotFoundException customNotFoundEx => (
                    StatusCodes.Status404NotFound,
                    ApiResponse<object>.ErrorResponse(customNotFoundEx.Message)
                ),

                // System Resource Not Found (Fallback)
                KeyNotFoundException notFoundEx => (
                    StatusCodes.Status404NotFound,
                    ApiResponse<object>.ErrorResponse(notFoundEx.Message)
                ),

                // Authorization Issues
                UnauthorizedAccessException _ => (
                    StatusCodes.Status401Unauthorized,
                    ApiResponse<object>.ErrorResponse("You are not authorized to perform this action.")
                ),

                // Catch-all for any other unhandled exceptions (System Exceptions, DB connections, etc.)
                // We intentionally do NOT expose the raw ex.Message to the client for security reasons.
                _ => (
                    StatusCodes.Status500InternalServerError,
                    ApiResponse<object>.ErrorResponse("An unexpected error occurred. Please contact support if the issue persists.")
                )
            };

            // Apply the determined status code and write the standardized JSON response
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}