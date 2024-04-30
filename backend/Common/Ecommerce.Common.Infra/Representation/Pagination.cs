﻿namespace Ecommerce.Common.Infra.Representation;

/// <summary>
/// Represents a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of items in the pagination.</typeparam>
public class Pagination<T>
{
    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int Page { get; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Gets the total number of items across all pages.
    /// </summary>
    public long TotalItems { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Gets or sets the list of items for the current page.
    /// </summary>
    public IEnumerable<T> Items { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Pagination{T}"/> class with the specified page information and items.
    /// </summary>
    /// <param name="page">The current page number.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="totalItems">The total number of items across all pages.</param>
    /// <param name="items">The list of items for the current page.</param>
    public Pagination(int page, int pageSize, long totalItems, IEnumerable<T> items)
    {
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = totalItems != 0L ? (int)Math.Round((float)totalItems / pageSize, MidpointRounding.ToPositiveInfinity) : 0;
        Items = items;
    }
}
