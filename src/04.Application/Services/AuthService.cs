using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Shared.Exceptions;
using AutoMapper;

namespace SupportTicketSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            var response = _mapper.Map<LoginResponseDto>(user);
            response.Token = "fake-jwt-token-for-assessment";

            return response;
        }
    }
}