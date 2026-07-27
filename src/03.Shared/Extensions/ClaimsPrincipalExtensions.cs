using System.Security.Claims;

namespace SupportTicketSystem.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                      ?? user.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(idClaim) || !Guid.TryParse(idClaim, out var userId))
            throw new UnauthorizedAccessException("User ID claim is missing or invalid.");

        return userId;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Role)?.Value
               ?? throw new UnauthorizedAccessException("Role claim is missing.");
    }
}