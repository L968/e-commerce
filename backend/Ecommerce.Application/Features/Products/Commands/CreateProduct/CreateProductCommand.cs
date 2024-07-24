using Ecommerce.Application.DTOs.Products;

namespace Ecommerce.Application.Features.Products.Commands.CreateProduct;

[Authorize]
public record CreateProductCommand : IRequest<GetProductDto>
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public Guid ProductCategoryId { get; set; }
    public bool? Active { get; set; }
    public bool? Visible { get; set; }
}

public class CreateProductCommandHandler(
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IProductRepository productRepository,
    IProductCategoryRepository productCategoryRepository
    ) : IRequestHandler<CreateProductCommand, GetProductDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository = productCategoryRepository;

    public async Task<GetProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(request.ProductCategoryId);
        DomainException.ThrowIfNull(productCategory, request.ProductCategoryId);

        var product = new Product(
            request.Name,
            request.Description,
            request.Active!.Value,
            request.Visible!.Value,
            productCategory.Id
        );

        _productRepository.Create(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetProductDto>(product);
    }
}
