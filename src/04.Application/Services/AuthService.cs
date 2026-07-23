using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SupportTicketSystem.Application.Abstractions.Services;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Shared.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SupportTicketSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IValidator<LoginRequestDto> _loginValidator;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IValidator<LoginRequestDto> loginValidator, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _userRepository = userRepository;
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

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new FluentValidation.ValidationException("Email atau password salah.");
            }

            var (token, expiresAt) = GenerateJwtToken(user);

            var response = _mapper.Map<LoginResponseDto>(user);
            response.Token = token;
            return response;
        }


        private (string Token, DateTime ExpiresAt) GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var expiresAt = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
        }
    }
}