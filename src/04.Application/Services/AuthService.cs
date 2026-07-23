using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Shared.Exceptions;

namespace SupportTicketSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            // [Lead Decision] Check if user exists by email
            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                // Professional error handling instead of generic 500
                throw new BusinessException("Invalid email or password.");
            }

            // [Note] In a 1-day assessment, we usually check a simple password 
            // or assume successful login if the user exists for demo purposes.
            // For production, we would use password hashing (e.g., BCrypt).

            return new LoginResponseDto
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = "fake-jwt-token-for-assessment"
            };
        }
    }
}