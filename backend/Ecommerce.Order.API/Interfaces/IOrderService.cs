using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.OrderCheckout;
using Ecommerce.Domain.Entities;
using FluentResults;

namespace Ecommerce.Order.API.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetPendingOrdersAsync();
    Task<Pagination<OrderDto>> GetByUserIdAsync(int userId, int page, int pageSize);
    Task<OrderDto?> GetByIdAsync(Guid id, int userId);
    Task<Result<string>> CreateOrderAsync(OrderCheckoutDto order);
    Task<Result> ProcessPayPalReturnAsync(string token);
    Task<Result> ProcessPayPalCancelAsync(string token);
}
