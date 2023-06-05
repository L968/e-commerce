namespace Ecommerce.Application.Interfaces.ProductServices;

public interface IProductService
{
    public Task<IEnumerable<GetProductDto>> GetAllAsync();
    public Task<GetProductDto?> GetByGuidAsync(Guid guid);
    public Task<Result<GetProductDto>> CreateAsync(CreateProductDto productDto);
    public Task<Result> UpdateAsync(Guid guid, UpdateProductDto productDto);
    public Task<Result> DeleteAsync(Guid guid);
}