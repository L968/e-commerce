using Ecommerce.Common.Infra.Repositories;
using Ecommerce.Domain.Enums;
using Ecommerce.Order.API.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Order.API.Repositories;

public class OrderRepository(AppDbContext context) : BaseRepository<AppDbContext, Domain.Entities.OrderEntities.Order>(context), IOrderRepository
{
    public async Task<(IEnumerable<Domain.Entities.OrderEntities.Order>, long TotalItems)> GetByUserIdAsync(int userId, int page, int pageSize)
    {
        var query = _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt);

        var totalItems = await query.CountAsync();

        var orders = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (orders, totalItems);
    }

    public async Task<OrderStatusCountDto> GetStatusCountAsync()
    {
        var statusCounts = await _context.Orders
            .GroupBy(o => o.Status)
            .Select(g => new
            {
                Status = g.Key,
                Count = g.Sum(o => o.Status == g.Key ? 1 : 0)
            })
            .ToDictionaryAsync(x => x.Status, x => x.Count);

        return new OrderStatusCountDto
        {
            PendingPaymentCount = statusCounts.GetValueOrDefault(OrderStatus.PendingPayment),
            ProcessingCount = statusCounts.GetValueOrDefault(OrderStatus.Processing),
            ShippedCount = statusCounts.GetValueOrDefault(OrderStatus.Shipped)
        };
    }

    public override async Task<Domain.Entities.OrderEntities.Order?> GetByIdAsync(Guid id)
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

    public async Task<Domain.Entities.OrderEntities.Order?> GetByExternalPaymentIdAsync(string externalPaymentId)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.ExternalPaymentId == externalPaymentId);
    }

    public async Task CreateAsync(Domain.Entities.OrderEntities.Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.OrderEntities.Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}
