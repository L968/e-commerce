using Ecommerce.Application.Addresses.Queries;
using Ecommerce.Application.CartItems.Queries;
using Ecommerce.Application.Carts.Queries;

namespace Ecommerce.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
	public DomainToDTOMappingProfile()
	{
        CreateMap<Address, GetAddressDto>();
        CreateMap<CartItem, GetCartItemDto>();
        CreateMap<Cart, GetCartDto>();
    }
}