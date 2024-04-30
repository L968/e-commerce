using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Common.Infra;

public static class EFCoreExtensions
{
    /// <summary>
    ///     Retrieves a page of items from the <paramref name="query"/> based on the specified <paramref name="page"/> and <paramref name="pageSize"/>.
    /// </summary>
    /// <typeparam name="T">The type of items in the query.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="page">The page number (1-based) to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A tuple containing the items for the specified page and the total number of items in the query.</returns>
    public static async Task<(IEnumerable<T> Items, long TotalItems)> ToResultAsync<T>(this IQueryable<T> query, int page, int pageSize)
    {
        var totalItems = await query.CountAsync();

        query = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var items = await query.ToListAsync();

        return (items, totalItems);
    }
}
