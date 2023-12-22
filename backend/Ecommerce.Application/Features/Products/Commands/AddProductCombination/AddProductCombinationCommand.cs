﻿using Ecommerce.Application.DTOs.Products;
using Ecommerce.Application.Interfaces;
using Ecommerce.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.AddProductCombination;

[Authorize]
public record AddProductCombinationCommand : IRequest<Result<GetProductCombinationDto>>
{
    public Guid ProductId { get; set; }
    public List<int> VariantOptionIds { get; set; } = null!;
    public string Sku { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }

    [DataType(DataType.Upload)]
    [MaxFileSize(5 * 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    public IFormFileCollection Images { get; set; } = null!;
}

public class AddProductCombinationCommandHandler : IRequestHandler<AddProductCombinationCommand, Result<GetProductCombinationDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IProductRepository _productRepository;
    private readonly IVariantOptionRepository _variantOptionRepository;
    private readonly IProductCombinationRepository _productCombinationRepository;

    public AddProductCombinationCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IBlobStorageService blobStorageService,
        IProductRepository productRepository,
        IVariantOptionRepository variantOptionRepository,
        IProductCombinationRepository productCombinationRepository
       )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
        _productRepository = productRepository;
        _variantOptionRepository = variantOptionRepository;
        _productCombinationRepository = productCombinationRepository;
    }

    public async Task<Result<GetProductCombinationDto>> Handle(AddProductCombinationCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null) return Result.Fail(DomainErrors.NotFound(nameof(Product), request.ProductId));

        var variantOptions = new List<VariantOption>();

        foreach (var variantOptionId in request.VariantOptionIds)
        {
            VariantOption? variantOption = await _variantOptionRepository.GetByIdAsync(variantOptionId);
            if (variantOption is null) return Result.Fail(DomainErrors.NotFound(nameof(VariantOption), variantOptionId));

            variantOptions.Add(variantOption);
        }

        List<string> imagePaths = await _blobStorageService.UploadImage(request.Images);

        var createResult = ProductCombination.Create(
            productId: product.Id,
            combinationString: GenerateCombinationString(variantOptions),
            sku: request.Sku,
            price: request.Price,
            stock: request.Stock,
            length: request.Length,
            width: request.Width,
            height: request.Height,
            weight: request.Weight,
            imagePaths: imagePaths
        );

        if (createResult.IsFailed) return Result.Fail(createResult.Errors);

        _productCombinationRepository.Create(createResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<GetProductCombinationDto>(createResult.Value);
        return Result.Ok(dto);
    }

    private static string GenerateCombinationString(List<VariantOption> variantOptions)
    {
        var combinationStrings = variantOptions.Select(vo => $"{vo.Variant!.Name}={vo.Name}");
        return string.Join("/", combinationStrings);
    }
}
