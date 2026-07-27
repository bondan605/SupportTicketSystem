using AutoMapper;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.DTOs;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Shared.DTOs.Tickets;
using SupportTicketSystem.Shared.DTOs.Users;

namespace SupportTicketSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket, TicketDto>();

            CreateMap<User, LoginResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreateTicketDto, Ticket>();

            CreateMap<User, UserDto>();

            CreateMap<TicketHistory, TicketHistoryDto>()
            .ForMember(dest => dest.TicketNumber, opt => opt.MapFrom(src => src.Ticket != null ? src.Ticket.TicketNumber : null))
            .ForMember(dest => dest.ChangedByName, opt => opt.MapFrom(src => src.ChangedByUser != null ? src.ChangedByUser.Name : null));
        }
    }
}