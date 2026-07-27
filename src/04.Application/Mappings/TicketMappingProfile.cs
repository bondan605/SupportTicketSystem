using AutoMapper;
using SupportTicketSystem.Domain.Entities;
using SupportTicketSystem.Shared.DTOs.Tickets;

namespace SupportTicketSystem.Application.Mappings
{
    /// <summary>
    /// AutoMapper configuration for everything mapping to/from <see cref="Ticket"/>.
    /// </summary>
    public class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<CreateTicketDto, Ticket>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Impact, opt => opt.MapFrom(src => src.Impact))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Application, opt => opt.MapFrom(src => src.Application))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo))
                .ForMember(dest => dest.EstimatedDueDate, opt => opt.MapFrom(src => src.EstimatedDueDate))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TicketNumber, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.ClosedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Assignee, opt => opt.Ignore()) 
                .ForMember(dest => dest.Histories, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());

            CreateMap<Ticket, TicketDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TicketNumber, opt => opt.MapFrom(src => src.TicketNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Impact, opt => opt.MapFrom(src => src.Impact))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Application, opt => opt.MapFrom(src => src.Application))
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo))
                .ForMember(dest => dest.EstimatedDueDate, opt => opt.MapFrom(src => src.EstimatedDueDate))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy));

            CreateMap<UpdateTicketDto, Ticket>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Impact, opt => opt.MapFrom(src => src.Impact))
                .ForMember(dest => dest.Application, opt => opt.MapFrom(src => src.Application))
                .ForMember(dest => dest.EstimatedDueDate, opt => opt.MapFrom(src => src.EstimatedDueDate))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TicketNumber, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerName, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerEmail, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerPhone, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedTo, opt => opt.Ignore())
                .ForMember(dest => dest.ClosedAt, opt => opt.Ignore())
                .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo))
                .ForMember(dest => dest.Histories, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore());
        }
    }
}