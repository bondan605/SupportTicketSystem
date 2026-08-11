using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.Constants;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Users;
using SupportTicketSystem.Shared.Models;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SupportTicketSystem.Client.Clients
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
        }

        //public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserAsync()
        //{
        //    try
        //    {
        //        // Sisipkan _jsonOptions pada parameter kedua
        //        var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>(ApiRoutes.Users.Base, _jsonOptions);

        //        return response ?? new ApiResponse<IEnumerable<UserDto>>
        //        {
        //            Success = false,
        //            Message = "No response received from the server."
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<IEnumerable<UserDto>>
        //        {
        //            Success = false,
        //            Message = $"Client Error: {ex.Message}"
        //        };
        //    }
        //}

        public async Task<ApiResponse<PagedResult<UserResponseDto>>?> GetAllUsersDetailAsync(
            PagedRequest request,
            string? searchString = null,
            UserRole? role = null,
            bool? status = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var url = $"{ApiRoutes.Users.Base}?pageNumber={request.PageNumber}&pageSize={request.PageSize}";

            if (!string.IsNullOrWhiteSpace(searchString))
                url += $"&searchString={Uri.EscapeDataString(searchString)}";
            if (role.HasValue)
                url += $"&role={role.Value}";
            if (status.HasValue)
                url += $"&status={status.Value}";
            if (startDate.HasValue)
                url += $"&startDate={startDate.Value:yyyy-MM-dd}";
            if (endDate.HasValue)
                url += $"&endDate={endDate.Value:yyyy-MM-dd}";

            return await _httpClient.GetFromJsonAsync<ApiResponse<PagedResult<UserResponseDto>>>(url, _jsonOptions);
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUserByRoleAsync(UserRole role)
        {
            try
            {
                // Sisipkan _jsonOptions pada parameter kedua
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>(ApiRoutes.Users.Role, _jsonOptions);
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

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllAgentsAsync()
        {
            try
            {
                // Sisipkan _jsonOptions pada parameter kedua
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<UserDto>>>(ApiRoutes.Users.Agent, _jsonOptions);
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

        public async Task<ApiResponse<UserResponseDto>> CreateUserAsync(CreateUserRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Users.Base, request, _jsonOptions);

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserResponseDto>>(_jsonOptions);

                return result ?? new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = "No response received from the server."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = $"Client Error: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<UserResponseDto>> UpdateUserAsync(Guid id, UpdateUserRequest request)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{ApiRoutes.Users.Base}/{id}", request, _jsonOptions);
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<UserResponseDto>>(_jsonOptions);

                return result ?? new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = "No response received from the server."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = $"Client Error: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse<UserResponseDto>> GetUserDetailByIdAsync(Guid id)
        {
            try
            {
                // Sisipkan _jsonOptions pada parameter kedua
                var response = await _httpClient.GetFromJsonAsync<ApiResponse<UserResponseDto>>(
                                $"api/users/detail/{id}",
                                _jsonOptions
                            );
                return response ?? new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = "No response received from the server."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponseDto>
                {
                    Success = false,
                    Message = $"Client Error: {ex.Message}"
                };
            }
        }
    }
}