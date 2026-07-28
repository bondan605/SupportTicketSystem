using System.Net;

namespace SupportTicketSystem.Client;

/// <summary>
/// Reactive safety net for token expiry: if the API ever responds 401 to an authenticated
/// request (expired/revoked token, clock skew, etc.), notifies <see cref="ISessionExpiryNotifier"/>
/// so the host app can show the "session expired" dialog. Complements the proactive,
/// schedule-based check the host app runs from the token's own expiry time - this handler
/// only catches it after the fact, once a real request has failed.
/// </summary>
public class SessionExpiryHandler : DelegatingHandler
{
    private readonly ISessionExpiryNotifier _notifier;

    public SessionExpiryHandler(ISessionExpiryNotifier notifier)
    {
        _notifier = notifier;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _notifier.NotifyExpired();
        }

        return response;
    }
}
