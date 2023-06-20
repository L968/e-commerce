using System.Transactions;

namespace Ecommerce.Application.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly IProductImageService _productImageService;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository,
        IProductCategoryRepository productCategoryRepository,
        IProductImageService productImageService,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _productImageService = productImageService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetProductDto>> GetAllAsync()
    {
        IEnumerable<Product> producs = await _productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetProductDto>>(producs);

        //return _productRepository.GetAllAsync()
        //    .Include(product => product.ProductCategory)
        //    .Include(product => product.ProductInventory)
        //    .Include(product => product.Images)
        //    .Select(product => product.ToDto());
    }

    public async Task<GetProductDto?> GetByGuidAsync(Guid guid)
    {
        Product? product = await _productRepository.GetByGuidAsync(guid);
        return _mapper.Map<GetProductDto?>(product);

        //return _context.Products
        //    .Include(product => product.ProductCategory)
        //    .Include(product => product.ProductInventory)
        //    .Include(product => product.Images)
        //    .FirstOrDefault(product => product.Guid == guid)?
        //    .ToDto();
    }

    public async Task<Result<GetProductDto>> CreateAsync(CreateProductDto productDto)
    {
        using var transaction = new TransactionScope();

        Product product = _mapper.Map<Product>(productDto);
        //ProductCategory? productCategory = await _productCategoryRepository.GetByIdAsync(product.ProductCategoryId);

        //if (productCategory == null) return Result.Fail("Product category not found");

        await _productRepository.CreateAsync(product);

        var result = await _productImageService.UploadImages(product.Id, productDto.Images!);

        if (result.IsFailed) return Result.Fail(result.Errors);

        product.Images = result.Value;

        transaction.Complete();
        GetProductDto getProductDto = _mapper.Map<GetProductDto>(product);
        return Result.Ok(getProductDto);
    }

    public async Task<Result> UpdateAsync(Guid guid, UpdateProductDto productDto)
    {
        Product? product = await _productRepository.GetByGuidAsync(guid);

        if (product == null)
        {
            return Result.Fail("Product not found");
        }

        _mapper.Map(productDto, product);

        await _productRepository.UpdateAsync(product);
        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(Guid guid)
    {
        throw new NotImplementedException("Block if product has orders");
        Product? product = await _productRepository.GetByGuidAsync(guid);

        if (product == null)
        {
            return Result.Fail("Product not found");
        }

        await _productRepository.DeleteAsync(product);
        return Result.Ok();
    }
}