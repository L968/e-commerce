using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Order.API.Repositories;

namespace Ecommerce.Order.API.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;

    public OrderService(IMapper mapper, IOrderRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id, int userId)
    {
        var order = await _repository.GetByIdAsync(id, userId);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId)
    {
        var orders = await _repository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<IEnumerable<OrderDto>> GetPendingOrdersAsync()
    {
        var orders = await _repository.GetPendingOrdersAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
