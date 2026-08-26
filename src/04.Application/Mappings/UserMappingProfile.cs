using AutoMapper;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Application.Mappings
{
    /// <summary>
    /// AutoMapper configuration for everything mapping to/from <see cref="User"/>.
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(dest => dest.TanggalDibuat, opt => opt.MapFrom(src => src.CreatedAt));

            CreateMap<User, LoginResponseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.ExpiresAt, opt => opt.Ignore());
        }
    }
}
