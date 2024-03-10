using Ecommerce.Domain.Enums;
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

    public async Task<IEnumerable<Domain.Entities.OrderEntities.Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Include(o => o.History)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id, int userId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Include(o => o.History)
            .FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
    }
}
