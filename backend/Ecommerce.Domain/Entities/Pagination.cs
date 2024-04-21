namespace Ecommerce.Domain.Entities;

public class Pagination<T>
{
    public int Page { get; }
    public int PageSize { get; }
    public long TotalItems { get; }
    public int TotalPages { get; }
    public IEnumerable<T> Items { get; set; }

    public Pagination(int page, int pageSize, long totalItems, IEnumerable<T> items)
    {
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = (totalItems != 0L) ? ((int)Math.Round((float)totalItems / pageSize, MidpointRounding.ToPositiveInfinity)) : 0;
        Items = items;
    }
}
