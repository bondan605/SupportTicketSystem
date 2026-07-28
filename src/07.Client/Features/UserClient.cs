using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Domain.Enums;
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

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>(ApiRoutes.Users.Base);

                return response ?? new ApiResponse<IEnumerable<UserDto>>
                {
                    Success = false,
                    Message = "No response received from the server."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<UserDto>>
                {
                    Success = false,
                    Message = $"Client Error: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserByRoleAsync(UserRole role)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>(ApiRoutes.Users.Role);
                return response ?? new ApiResponse<IEnumerable<UserDto>>
                {
                    Success = false,
                    Message = "No response received from the server."
                };
            }
            catch (Exception ex) 
            {
                return new ApiResponse<IEnumerable<UserDto>>
                {
                    Success = false,
                    Message = $"Client Error: {ex.Message}"
                };
            }
        }
    }
}
