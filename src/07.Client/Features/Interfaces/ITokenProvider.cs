namespace SupportTicketSystem.Client.Features.Interfaces
{
    public interface ITokenProvider
    {
        Task<string?> GetTokenAsync();
        Task SetTokenAsync(string token);
        Task DeleteTokenAsync();
    }
}
