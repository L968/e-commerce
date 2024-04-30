using Ecommerce.Common.Infra.Exceptions;
using Ecommerce.Common.Infra.Representation.Grid;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Ecommerce.Common.Infra.Repositories;

/// <summary>
/// Base repository implementation for generic repository operations using Entity Framework Core.
/// </summary>
/// <typeparam name="TContext">The type of the database context.</typeparam>
/// <typeparam name="T">The type of entity.</typeparam>
public class BaseRepository<TContext, T>(TContext context)
    : IBaseRepository<T>
        where TContext : DbContext
        where T : class
{
    protected readonly TContext _context = context;

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public virtual async Task<(IEnumerable<T>, long TotalItems)> GetAllAsync(GridParams gridParams)
    {
        return await ApplyGridParams(gridParams)
            .ToResultAsync(gridParams.Page, gridParams.PageSize);
    }
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);

    }
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public IQueryable<T> ApplyGridParams(GridParams gridParams)
    {
        try
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var filter in gridParams.Filters)
            {
                query = ApplyFilter(query, filter);
            }

            foreach (var sort in gridParams.Sorters)
            {
                query = ApplySort(query, sort);
            }

            return query;
        }
        catch (Exception ex)
            when (ex is FormatException
               || ex is ArgumentException
               || ex is InvalidOperationException)
        {
            throw new GridParamsException(ex.Message);
        }
    }
    public virtual T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }
    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    /// <summary>
    /// Applies a filter to the query based on the provided <paramref name="filter"/>.
    /// </summary>
    /// <param name="query">The query to apply the filter to.</param>
    /// <param name="filter">The filter parameters specifying the property to filter on, the operator, and the value.</param>
    /// <returns>The filtered query.</returns>
    protected static IQueryable<T> ApplyFilter(IQueryable<T> query, FilterParams filter)
    {
        var property = GetPropertyInfo(filter.Property);

        var parameter = Expression.Parameter(typeof(T));
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);

        Expression expression;
        if (filter.Operator == "like")
        {
            expression = BuildLikeExpression(propertyAccess, filter.Value);
        }
        else
        {
            var value = GetConvertedValue(property.PropertyType, filter.Value);
            expression = BuildComparisonExpression(filter.Operator, propertyAccess, value);
        }

        var lambda = Expression.Lambda<Func<T, bool>>(expression, parameter);
        return query.Where(lambda);
    }

    /// <summary>
    /// Applies sorting to the specified query using the provided sort parameters.
    /// </summary>
    /// <param name="query">The query to apply sorting to.</param>
    /// <param name="sorter">The sort parameters specifying the property to sort on and the sort direction ("asc" or "desc").</param>
    /// <returns>The sorted query.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified property is not found in the type.</exception>
    /// <exception cref="InvalidOperationException">Thrown when no suitable sorting method is found for the specified type.</exception>
    protected static IQueryable<T> ApplySort(IQueryable<T> query, SortParams sorter)
    {
        var property = GetPropertyInfo(sorter.Property)
            ?? throw new ArgumentException($"Property {sorter.Property} not found in type {typeof(T).Name}");

        var parameter = Expression.Parameter(typeof(T));
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var lambda = Expression.Lambda(propertyAccess, parameter);

        var methodName = sorter.Direction.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

        var method = typeof(Queryable)
            .GetMethods()
            .FirstOrDefault(m => m.Name == methodName
                && m.IsGenericMethodDefinition
                && m.GetGenericArguments().Length == 2
            );

        if (method is null)
            throw new InvalidOperationException($"No method found for {methodName} in type {typeof(T).Name}");

        method = method.MakeGenericMethod(typeof(T), property.PropertyType);

        return (IQueryable<T>)method.Invoke(null, [query, lambda]);
    }

    /// <summary>
    /// Gets the converted value of the provided <paramref name="value"/> based on the <paramref name="propertyType"/>.
    /// </summary>
    /// <param name="propertyType">The type of the property.</param>
    /// <param name="value">The value to convert.</param>
    /// <returns>The converted value.</returns>
    private static object GetConvertedValue(Type propertyType, string value)
    {
        if (propertyType.IsEnum)
        {
            var enumValue = Enum.Parse(propertyType, value);

            if (!Enum.IsDefined(propertyType, enumValue))
            {
                throw new ArgumentException($"Value '{value}' is not valid for enum type '{propertyType}'.");
            }

            return enumValue;
        }

        if (propertyType == typeof(Guid))
        {
            return Guid.Parse(value);
        }

        return Convert.ChangeType(value, propertyType);
    }

    /// <summary>
    /// Gets the <see cref="PropertyInfo"/> of the property with the provided <paramref name="propertyName"/>.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    /// <returns>The <see cref="PropertyInfo"/> of the property.</returns>
    private static PropertyInfo GetPropertyInfo(string propertyName)
    {
        var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
            ?? throw new ArgumentException($"Property {propertyName} not found in type {typeof(T).Name}");

        return property;
    }

    /// <summary>
    /// Builds an expression for a "like" comparison.
    /// </summary>
    /// <param name="propertyAccess">The member expression representing the property access.</param>
    /// <param name="value">The value to compare with.</param>
    /// <returns>The method call expression representing the "like" comparison.</returns>
    private static MethodCallExpression BuildLikeExpression(MemberExpression propertyAccess, object value)
    {
        var valueExpression = Expression.Constant(value);
        return Expression.Call(propertyAccess, "Contains", null, valueExpression);
    }

    /// <summary>
    /// Builds a comparison expression based on the provided operator.
    /// </summary>
    /// <param name="op">The comparison operator.</param>
    /// <param name="propertyAccess">The member expression representing the property access.</param>
    /// <param name="value">The value to compare with.</param>
    /// <returns>The binary expression representing the comparison.</returns>
    private static BinaryExpression BuildComparisonExpression(string op, MemberExpression propertyAccess, object value)
    {
        var constant = Expression.Constant(value);

        return op switch
        {
            "=" => Expression.Equal(propertyAccess, constant),
            ">" => Expression.GreaterThan(propertyAccess, constant),
            "<" => Expression.LessThan(propertyAccess, constant),
            ">=" => Expression.GreaterThanOrEqual(propertyAccess, constant),
            "<=" => Expression.LessThanOrEqual(propertyAccess, constant),
            _ => throw new ArgumentException($"Invalid operator: {op}"),
        };
    }
}
