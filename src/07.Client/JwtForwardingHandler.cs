using System.Net.Http.Headers;

namespace SupportTicketSystem.Client
{
    /// <summary>
    /// Attaches the current user's access token (supplied by the host app) as a Bearer
    /// token on every outgoing request. Kept dependency-free from ASP.NET Core so this
    /// library stays a plain class library; the host app supplies the token accessor.
    /// </summary>
    public class JwtForwardingHandler : DelegatingHandler
    {
        private readonly Func<Task<string?>> _accessTokenAccessor;

        public JwtForwardingHandler(Func<Task<string?>> accessTokenAccessor)
        {
            _accessTokenAccessor = accessTokenAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _accessTokenAccessor();
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
