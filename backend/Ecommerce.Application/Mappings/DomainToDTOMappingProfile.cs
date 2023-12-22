using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.DTOs.Variants;
using Ecommerce.Application.Features.Addresses.Queries;
using Ecommerce.Application.Features.CartItems.Queries;
using Ecommerce.Application.Features.Carts.Queries;

namespace Ecommerce.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
	public DomainToDTOMappingProfile()
	{
        CreateMap<Address, GetAddressDto>();

        CreateMap<Cart, GetCartDto>();
        CreateMap<CartItem, GetCartItemDto>();

        CreateMap<Product, GetProductAdminDto>();
        CreateMap<Product, GetProductDto>()
            .ForMember(dto => dto.Rating, opt => opt.MapFrom(p => p.GetRating()))
            .ForMember(dto => dto.Variants, opt => opt.MapFrom(p => p.VariantOptions.Select(vo => vo.VariantOption.Variant)));

        CreateMap<Product, GetProductListDto>()
            .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(p => p.Category.Name))
            .ForMember(dto => dto.OriginalPrice, opt => opt.MapFrom(p => p.Combinations[0].Price))
            .ForMember(dto => dto.DiscountedPrice, opt => opt.MapFrom(p => p.Combinations[0].GetDiscountedPrice()))
            .ForMember(dto => dto.ImageSource, opt => opt.MapFrom(p => p.Combinations[0].Images.ElementAt(0).ImagePath))
            .ForMember(dto => dto.Rating, opt => opt.MapFrom(p => p.GetRating()))
            .ForMember(dto => dto.Variants, opt => opt.MapFrom(p => p.VariantOptions.Select(vo => vo.VariantOption.Variant)))
            .ForMember(dto => dto.ReviewsCount, opt => opt.MapFrom(p => p.Reviews.Count));

        CreateMap<ProductImage, GetProductImageDto>();
        CreateMap<ProductCategory, GetProductCategoryDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(dto => dto.Variants, opt => opt.MapFrom(c => c.Variants.Select(v => v.Variant)));

        CreateMap<ProductDiscount, GetProductDiscountDto>();
        CreateMap<ProductCombination, GetProductCombinationDto>()
            .ForMember(dto => dto.OriginalPrice, opt => opt.MapFrom(pc => pc.Price))
            .ForMember(dto => dto.DiscountedPrice, opt => opt.MapFrom(pc => pc.GetDiscountedPrice()))
            .ForMember(dto => dto.Images, opt => opt.MapFrom(pc => pc.Images.Select(i => i.ImagePath)))
            .ForMember(dto => dto.Stock, opt => opt.MapFrom(pc => pc.Inventory.Stock));

        CreateMap<ProductCategoryVariant, GetProductCategoryVariantDto>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(pv => pv.Variant.Name))
            .ForMember(dto => dto.Options, opt => opt.MapFrom(pcv => pcv.Variant.Options.Select(vo => vo.Name)));

        CreateMap<ProductReview, GetProductReviewDto>();
        CreateMap<Variant, GetVariantDto>();

        CreateMap<VariantOption, GetVariantOptionDto>();
    }
}
