using Ecommerce.Domain.Entities.OrderEntities;
using Ecommerce.Order.API.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.API.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetPendingOrdersAsync()
    {
        return await _context.Orders
            .Where(o => o.Status == OrderStatus.PendingPayment)
            .ToListAsync();
    }

    public async Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }
}
