using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Shared.Exceptions;
using AutoMapper;
using FluentValidation;

namespace SupportTicketSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginRequestDto> _loginValidator;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LoginRequestDto> loginValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loginValidator = loginValidator;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            await _loginValidator.ValidateAndThrowAsync(dto);

            var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new BusinessException("Invalid email or password.");
            }

            var response = _mapper.Map<LoginResponseDto>(user);
            response.Token = "fake-jwt-token-for-assessment";

            return response;
        }
    }
}