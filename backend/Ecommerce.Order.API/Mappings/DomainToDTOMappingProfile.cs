using AutoMapper;
using Ecommerce.Domain.DTOs;
using Ecommerce.Domain.Entities.ProductEntities;
using Ecommerce.Domain.Enums;
using Ecommerce.Common.Infra.Extensions;

namespace Ecommerce.Order.API.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Domain.Entities.OrderEntities.Order, OrderDto>()
            .ForMember(dto => dto.ShippingAddress, opt => opt.MapFrom(o => $"{o.ShippingStreetName} {o.ShippingBuildingNumber}, {o.ShippingNeighborhood}, {o.ShippingCity}, {o.ShippingState}"))
            .ForMember(dto => dto.Status, opt => opt.MapFrom(o => o.Status.ToString().AddSpacesBeforeUpperCase()))
            .ForMember(dto => dto.PaymentMethod, opt => opt.MapFrom(
                o => o.PaymentMethod == PaymentMethod.PayPal
                    ? o.PaymentMethod.ToString()
                    : o.PaymentMethod.ToString().AddSpacesBeforeUpperCase()
             ))
            .ForMember(dto => dto.TotalAmount, opt => opt.MapFrom(o => o.GetTotalAmount()))
            .ForMember(dto => dto.Subtotal, opt => opt.MapFrom(o => o.Items.Sum(oi => oi.GetTotalAmount())));

        CreateMap<Domain.Entities.OrderEntities.OrderItem, OrderItemDto>()
            .ForMember(dto => dto.TotalAmount, opt => opt.MapFrom(oi => oi.GetTotalAmount()));
        ;
        CreateMap<Domain.Entities.OrderEntities.OrderHistory, OrderHistoryDto>()
            .ForMember(dto => dto.Status, opt => opt.MapFrom(o => o.Status.ToString().AddSpacesBeforeUpperCase()));

        CreateMap<CreateOrderProductDto, Product>();
        CreateMap<CreateOrderProductImageDto, ProductImage>();
        CreateMap<CreateOrderProductInventoryDto, ProductInventory>();
        CreateMap<CreateOrderProductDiscountDto, ProductDiscount>();
        CreateMap<CreateOrderProductCombinationDto, ProductCombination>();
    }
}
