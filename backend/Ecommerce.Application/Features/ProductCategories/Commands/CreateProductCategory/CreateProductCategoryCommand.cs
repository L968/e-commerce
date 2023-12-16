using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.ProductCategories.Commands.CreateProductCategory;

[Authorize]
public record CreateProductCategoryCommand : IRequest<GetProductCategoryDto>
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
}

public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, GetProductCategoryDto>
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

    public async Task<GetProductCategoryDto> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productCategory = new ProductCategory(request.Name, request.Description);

        _productCategoryRepository.Create(productCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetProductCategoryDto>(productCategory);
    }
}
