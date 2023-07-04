using Ecommerce.Application.Features.Addresses.Queries;
using Ecommerce.Application.Features.CartItems.Queries;
using Ecommerce.Application.Features.Carts.Queries;
using Ecommerce.Application.Features.ProductCategories.Queries;
using Ecommerce.Application.Features.ProductDiscounts.Queries;
using Ecommerce.Application.Features.Products.Queries;

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
        CreateMap<ProductDiscount, GetProductDiscountDto>();
    }
}