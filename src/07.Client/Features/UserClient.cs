using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Users;
using System.Net.Http.Json;

namespace SupportTicketSystem.Client.Clients
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllAgentsAsync()
        {
            return await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>(ApiRoutes.Users.Base) ?? new ApiResponse<IEnumerable<UserDto>> { Success = false };
        }
    }
}
