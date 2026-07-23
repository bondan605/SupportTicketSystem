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

            CreateMap<User, LoginResponseDto>();

            CreateMap<CreateTicketDto, Ticket>();

            CreateMap<User, UserDto>();
        }
    }
}