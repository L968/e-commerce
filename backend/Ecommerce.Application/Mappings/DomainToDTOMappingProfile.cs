using Ecommerce.Application.Addresses.Queries;
using Ecommerce.Application.CartItems.Queries;
using Ecommerce.Application.Carts.Queries;
using Ecommerce.Application.ProductCategories.Queries;
using Ecommerce.Application.Products.Queries;

namespace Ecommerce.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
	public DomainToDTOMappingProfile()
	{
        CreateMap<Address, GetAddressDto>();

        CreateMap<Cart, GetCartDto>();
        CreateMap<CartItem, GetCartItemDto>();

        CreateMap<Product, GetProductDto>();
        CreateMap<ProductImage, GetProductImageDto>();
        CreateMap<ProductCategory, GetProductCategoryDto>();
    }
}