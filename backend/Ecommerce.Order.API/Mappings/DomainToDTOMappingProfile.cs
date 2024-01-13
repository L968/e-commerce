using AutoMapper;
using Ecommerce.Application.DTOs;

namespace Ecommerce.Order.API.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Domain.Entities.OrderEntities.Order, OrderDto>()
            .ForMember(dto => dto.ShippingAddress, opt => opt.MapFrom(o => $"{o.ShippingStreetName} {o.ShippingBuildingNumber}, {o.ShippingNeighborhood}, {o.ShippingCity}, {o.ShippingState}")); ;

        CreateMap<Domain.Entities.OrderEntities.OrderItem, OrderItemDto>();
        CreateMap<Domain.Entities.OrderEntities.OrderHistory, OrderHistoryDto>();
    }
}
