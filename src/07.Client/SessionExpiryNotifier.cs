namespace SupportTicketSystem.Client;

/// <summary>
/// Scoped pub/sub used to signal that the current session's token is no longer valid,
/// either because the API rejected a request with 401 (<see cref="SessionExpiryHandler"/>)
/// or because a scheduled expiry check fired first. The host app subscribes to
/// <see cref="SessionExpired"/> to show the "session expired" dialog and force a re-login.
/// Kept dependency-free from ASP.NET Core so this library stays a plain class library.
/// </summary>
public interface ISessionExpiryNotifier
{
    event Action? SessionExpired;

    void NotifyExpired();
}

public class SessionExpiryNotifier : ISessionExpiryNotifier
{
    public event Action? SessionExpired;

    public void NotifyExpired() => SessionExpired?.Invoke();
}
