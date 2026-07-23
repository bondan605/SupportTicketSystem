using AutoMapper;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Shared.DTOs.Tickets;

namespace SupportTicketSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Ticket Mappings
            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // User Mappings for Auth
            CreateMap<User, LoginResponseDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}