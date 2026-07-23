using SupportTicketSystem.WebApi.Middleware;

namespace SupportTicketSystem.WebApi.Extensions
{
    // =============================================================================
    // MIDDLEWARE PIPELINE
    // =============================================================================
    // Middleware runs in the order it is registered. Each middleware component
    // can process an incoming request, pass it to the next component, and then
    // process the outgoing response on the way back.
    //
    // Request flow:
    //   Swagger → ExceptionHandler → HTTPS Redirect → Controllers
    // =============================================================================
    public static class MiddlewareExtensions
    {
        // Register the global exception handling middleware.
        // This intercepts unhandled exceptions thrown anywhere in the pipeline and
        // returns a consistent error response instead of exposing raw stack traces.
        // Must be registered early in the pipeline to catch exceptions from all
        // subsequent middleware and controllers.
        public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
