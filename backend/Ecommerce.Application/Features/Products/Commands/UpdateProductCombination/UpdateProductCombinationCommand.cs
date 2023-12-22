using Ecommerce.Application.Interfaces;
using Ecommerce.Utils.Attributes;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Products.Commands.UpdateProductCombination;

[Authorize]
public record UpdateProductCombinationCommand : IRequest<Result>
{
    public Guid Id { get; set; }
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

public class UpdateProductCombinationCommandHandler : IRequestHandler<UpdateProductCombinationCommand, Result>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobStorageService _blobStorageService;
    private readonly IProductCombinationRepository _productCombinationRepository;

    public UpdateProductCombinationCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IBlobStorageService blobStorageService,
        IProductCombinationRepository productCombinationRepository
    )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _blobStorageService = blobStorageService;
        _productCombinationRepository = productCombinationRepository;
    }

    public async Task<Result> Handle(UpdateProductCombinationCommand request, CancellationToken cancellationToken)
    {
        ProductCombination? productCombination = await _productCombinationRepository.GetByIdAsync(request.Id);
        if (productCombination is null) return Result.Fail(DomainErrors.NotFound(nameof(ProductCombination), request.Id));

        await _blobStorageService.RemoveImage(productCombination.Images.Select(i => i.ImagePath).ToList());

        List<string> imagePaths = await _blobStorageService.UploadImage(request.Images);

        var updateResult = productCombination.Update(
            sku: request.Sku,
            price: request.Price,
            length: request.Length,
            width: request.Width,
            height: request.Height,
            weight: request.Weight,
            imagePaths: imagePaths
        );

        if (updateResult.IsFailed) return updateResult;

        _productCombinationRepository.Update(productCombination);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
