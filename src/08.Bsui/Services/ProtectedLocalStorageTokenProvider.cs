using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SupportTicketSystem.Client.Features.Interfaces;

namespace SupportTicketSystem.Bsui.Services
{
    public class ProtectedLocalStorageTokenProvider : ITokenProvider
    {
        private const string TokenKey = "authToken";
        private readonly ProtectedLocalStorage _localStorage;

        public ProtectedLocalStorageTokenProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                var result = await _localStorage.GetAsync<string>(TokenKey);
                return result.Success ? result.Value : null;
            }
            catch
            {
                // JS interop belum siap (prerender) atau data corrupt
                return null;
            }
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetAsync(TokenKey, token);
        }

        public async Task DeleteTokenAsync()
        {
            await _localStorage.DeleteAsync(TokenKey);
        }
    }
}