using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;
using Ecommerce.Order.API.Repositories;

namespace Ecommerce.Order.API.Services;

public class OrderService(IMapper mapper, IOrderRepository repository) : IOrderService
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _repository = repository;

    public async Task<OrderDto?> GetByIdAsync(Guid id, int userId)
    {
        var order = await _repository.GetByIdAsync(id, userId);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<Pagination<OrderDto>> GetByUserIdAsync(int userId, int page, int pageSize)
    {
        var (orders, totalItems) = await _repository.GetByUserIdAsync(userId, page, pageSize);

        return new Pagination<OrderDto>(
            page,
            pageSize,
            totalItems,
            _mapper.Map<IEnumerable<OrderDto>>(orders)
        );
    }

    public async Task<IEnumerable<OrderDto>> GetPendingOrdersAsync()
    {
        var orders = await _repository.GetPendingOrdersAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
