using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductCategories.Commands.CreateProductCategory;

[Authorize]
public record CreateProductCategoryCommand : IRequest<Result<GetProductCategoryDto>>
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public List<Guid> VariantIds { get; set; } = null!;
}

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Result<GetProductCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductCategoryRepository _productCategoryRepository;

    public CreateProductCategoryCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IProductCategoryRepository productCategoryRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productCategoryRepository = productCategoryRepository;
    }

    public async Task<Result<GetProductCategoryDto>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = ProductCategory.Create(request.Name, request.Description, request.VariantIds);
        if (result.IsFailed) return Result.Fail(result.Errors);

        _productCategoryRepository.Create(result.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(_mapper.Map<GetProductCategoryDto>(result.Value));
    }
}
