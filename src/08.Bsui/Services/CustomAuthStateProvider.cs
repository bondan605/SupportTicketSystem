using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;
using SupportTicketSystem.Client.Features.Interfaces;

namespace SupportTicketSystem.Bsui.Services
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ITokenProvider _tokenProvider;

        public CustomAuthStateProvider(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _tokenProvider.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
                return Anonymous();

            try
            {
                var identity = ParseClaimsFromJwt(token);

                var expClaim = identity.FindFirst(c => c.Type == "exp")?.Value;
                if (expClaim != null && long.TryParse(expClaim, out var expUnix))
                {
                    var expDate = DateTimeOffset.FromUnixTimeSeconds(expUnix);
                    if (expDate < DateTimeOffset.UtcNow)
                    {
                        await _tokenProvider.DeleteTokenAsync();
                        return Anonymous();
                    }
                }

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            catch
            {
                // Token corrupt / tidak bisa di-parse
                return Anonymous();
            }
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _tokenProvider.SetTokenAsync(token);
            var identity = ParseClaimsFromJwt(token);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _tokenProvider.DeleteTokenAsync();
            NotifyAuthenticationStateChanged(Task.FromResult(Anonymous()));
        }

        private static AuthenticationState Anonymous() =>
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        private static ClaimsIdentity ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return new ClaimsIdentity(token.Claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);
        }
    }
}