using Microsoft.AspNetCore.Authentication;

namespace SupportTicketSystem.Bsui.Services
{
    /// <summary>
    /// Reads the access token stored in the auth cookie via HttpContext.
    /// HttpContext is only available while a request is in flight (initial page
    /// load / circuit reconnect); later SignalR-only interactions during the same
    /// circuit have no HttpContext, so the token is cached here once retrieved.
    /// </summary>
    public class ServerJwtAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string? _cachedToken;

        public ServerJwtAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string?> GetTokenAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is not null)
            {
                _cachedToken = await httpContext.GetTokenAsync("access_token");
            }

            return _cachedToken;
        }
    }
}
