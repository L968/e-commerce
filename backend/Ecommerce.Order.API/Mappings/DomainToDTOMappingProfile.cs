using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Order.API.Utils;

namespace Ecommerce.Order.API.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Domain.Entities.OrderEntities.Order, OrderDto>()
            .ForMember(dto => dto.ShippingAddress, opt => opt.MapFrom(o => $"{o.ShippingStreetName} {o.ShippingBuildingNumber}, {o.ShippingNeighborhood}, {o.ShippingCity}, {o.ShippingState}"))
            .ForMember(dto => dto.Status, opt => opt.MapFrom(o => StringManipulationUtils.AddSpacesBeforeUpperCase(o.Status.ToString())));

        CreateMap<Domain.Entities.OrderEntities.OrderItem, OrderItemDto>();
        CreateMap<Domain.Entities.OrderEntities.OrderHistory, OrderHistoryDto>();
    }
}
