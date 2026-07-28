using AutoMapper;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserByRoleAsync(UserRole role)
        {
            var users = await _userRepository.GetAllByRoleAsync(role);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> GetAllAgentsAsync()
        {
            var users = await _userRepository.GetAllAgentsAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}
