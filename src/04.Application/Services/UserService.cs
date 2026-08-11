using AutoMapper;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Users;
using SupportTicketSystem.Shared.Models;

namespace SupportTicketSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        //{
        //    var users = await _unitOfWork.Users.GetAllAsync();
        //    return _mapper.Map<IEnumerable<UserDto>>(users);
        //}

        public async Task<PagedResult<UserResponseDto>> GetAllUsersDetailAsync(
            string currentUserRole,
            PagedRequest request,
            string? searchString,
            UserRole? role,
            bool? status,
            DateTime? startDate,
            DateTime? endDate)
        {
            var (users, totalCount) = await _unitOfWork.Users.GetPagedUsersAsync(
                request.PageNumber,
                request.PageSize,
                currentUserRole,
                searchString,
                role,
                status,
                startDate,
                endDate);

            var userDtos = _mapper.Map<IEnumerable<UserResponseDto>>(users);

            return new PagedResult<UserResponseDto>
            {
                Items = userDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public async Task<IEnumerable<UserDto>> GetAllUserByRoleAsync(UserRole role)
        {
            var users = await _unitOfWork.Users.GetAllByRoleAsync(role);
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<IEnumerable<UserDto>> GetAllAgentsAsync()
        {
            var users = await _unitOfWork.Users.GetAllAgentsAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
        public async Task<UserResponseDto> CreateUserAsync(CreateUserRequest request)
        {
            if (await _unitOfWork.Users.ExistsByEmailAsync(request.Email))
            {
                throw new InvalidOperationException("Email sudah terdaftar.");
            }

            if (await _unitOfWork.Users.ExistsByUsernameAsync(request.Username))
            {
                throw new InvalidOperationException("Username sudah digunakan.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Name = request.Name,
                Username = request.Username,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = request.Role,
                PhoneNumber = request.PhoneNumber,
                BirthDate = request.BirthDate,
                JobTitle = request.JobTitle,
                Address = request.Address,
                IsActive = true
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto> UpdateUserAsync(Guid id, UpdateUserRequest request, string currentUserRole)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("Pengguna tidak ditemukan.");
            }

            // Validasi hak akses (Manager hanya bisa update Support Agent)
            if (currentUserRole == "Manager" && user.Role != UserRole.SupportAgent)
            {
                throw new UnauthorizedAccessException("Manager hanya dapat mengedit Support Agent.");
            }

            user.Name = request.Name;
            user.Role = request.Role;
            user.PhoneNumber = request.PhoneNumber;
            user.BirthDate = request.BirthDate;
            user.JobTitle = request.JobTitle;
            user.Address = request.Address;
            user.IsActive = request.Status;

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<UserResponseDto?> GetUserDetailByIdAsync(Guid id, string currentUserRole)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return null;

            if (currentUserRole == "Manager" && user.Role != UserRole.SupportAgent)
            {
                throw new UnauthorizedAccessException("Manager hanya dapat melihat detail Support Agent.");
            }

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<Dictionary<Guid, string>> GetUserNameDictionaryAsync(IEnumerable<Guid> userIds)
        {
            var distinctIds = userIds?.Distinct().ToList();

            if (distinctIds == null || !distinctIds.Any())
            {
                return new Dictionary<Guid, string>();
            }

            // Delegate the data-fetching logic to the repository layer
            return await _unitOfWork.Users.GetUserNameDictionaryAsync(distinctIds);
        }
    }
}
