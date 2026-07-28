using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

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

        /// <summary>The token's own "exp" claim (UTC), used to schedule the proactive
        /// session-expiry check. Returns null if there's no token or it can't be read.</summary>
        public async Task<DateTime?> GetTokenExpiryAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            try
            {
                return new JwtSecurityTokenHandler().ReadJwtToken(token).ValidTo;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
