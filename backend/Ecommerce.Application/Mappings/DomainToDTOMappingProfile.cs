using Ecommerce.Application.DTOs.Addresses;
using Ecommerce.Application.DTOs.Carts;
using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.DTOs.Products.Admin;
using Ecommerce.Application.DTOs.Variants;

namespace Ecommerce.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
	public DomainToDTOMappingProfile()
	{
        CreateMap<Address, GetAddressDto>();

        CreateMap<Cart, GetCartDto>();
        CreateMap<CartItem, GetCartItemDto>()
            .ForMember(dto => dto.Product, opt => opt.MapFrom(ci => ci.ProductCombination));

        CreateMap<Product, GetProductAdminDto>();
        CreateMap<Product, GetProductByIdDto>();
        CreateMap<Product, GetProductDto>()
            .ForMember(dto => dto.Rating, opt => opt.MapFrom(p => p.GetRating()))
            .ForMember(dto => dto.Variants, opt => opt.MapFrom(p => p.VariantOptions.Select(vo => vo.VariantOption.Variant).GroupBy(v => v.Id).Select(g => g.First())));

        CreateMap<Product, GetProductListDto>()
            .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(p => p.Category.Name))
            .ForMember(dto => dto.OriginalPrice, opt => opt.MapFrom(p => p.Combinations.Any() ? p.Combinations.ElementAt(0).Price : (decimal?)null))
            .ForMember(dto => dto.DiscountedPrice, opt => opt.MapFrom(p => p.Combinations.Any() ? p.Combinations.ElementAt(0).GetDiscountedPrice() : (decimal?)null))
            .ForMember(dto => dto.ImageSource, opt => opt.MapFrom(p => p.Combinations.Any() ? p.Combinations.ElementAt(0).Images.ElementAt(0).ImagePath : null))
            .ForMember(dto => dto.Rating, opt => opt.MapFrom(p => p.GetRating()))
            .ForMember(dto => dto.Variants, opt => opt.MapFrom(p => p.VariantOptions.Select(vo => vo.VariantOption.Variant)))
            .ForMember(dto => dto.ReviewsCount, opt => opt.MapFrom(p => p.Reviews.Count));

        CreateMap<ProductImage, GetProductImageDto>();
        CreateMap<ProductInventory, GetProductInventoryDto>();
        CreateMap<ProductCategory, GetProductCategoryDto>()
            .ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id))
            .ForMember(dto => dto.Variants, opt => opt.MapFrom(c => c.Variants.Select(v => v.Variant)));

        CreateMap<ProductDiscount, GetProductDiscountDto>();

        CreateMap<ProductCombination, GetProductCombinationByIdDto>();
        CreateMap<ProductCombination, GetProductCombinationDto>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(pc => pc.Product.Name))
            .ForMember(dto => dto.OriginalPrice, opt => opt.MapFrom(pc => pc.Price))
            .ForMember(dto => dto.DiscountedPrice, opt => opt.MapFrom(pc => pc.GetDiscountedPrice()))
            .ForMember(dto => dto.Images, opt => opt.MapFrom(pc => pc.Images.Select(i => i.ImagePath)))
            .ForMember(dto => dto.Stock, opt => opt.MapFrom(pc => pc.Inventory.Stock));

        CreateMap<ProductCombination, GetProductCombinationAdminDto>()
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
