using Ecommerce.Common.Infra.Representation.Grid;

namespace Ecommerce.Common.Infra.Repositories;

/// <summary>
/// Interface for generic repository operations.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface IBaseRepository<T>
{
    /// <summary>
    /// Gets all entities of type <typeparamref name="T"/> asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns a collection of entities.</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Executes a query using the specified grid parameters and returns the result asynchronously.
    /// </summary>
    /// <param name="gridParams">The grid parameters containing filters, sorters, and pagination settings.</param>
    /// <returns>A tuple containing the result items and the total number of items.</returns>
    /// <remarks>
    /// To include related entities, use the ApplyGridParams method and then use the ToResultAsync method to execute the query.
    /// </remarks>
    Task<(IEnumerable<T>, long TotalItems)> GetAllAsync(GridParams gridParams);

    /// <summary>
    /// Gets an entity of type <typeparamref name="T"/> by its integer id asynchronously.
    /// </summary>
    /// <param name="id">The integer id of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns the entity, or <c>null</c> if not found.</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Gets an entity of type <typeparamref name="T"/> by its GUID id asynchronously.
    /// </summary>
    /// <param name="id">The GUID id of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation that returns the entity, or <c>null</c> if not found.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    ///     Applies the specified grid parameters to the query, including filters, sorters, and pagination settings.
    /// </summary>
    /// <param name="gridParams">The grid parameters containing filters, sorters, and pagination settings.</param>
    /// <remarks>
    ///     This method is used to dynamically apply grid params settings to the query, as well as <b>chain EF Core Includes for related entities.</b>
    /// </remarks>
    /// <returns>The IQueryable instance with the specified grid parameters applied.</returns>
    /// <exception cref="GridParamsException"></exception>
    IQueryable<T> ApplyGridParams(GridParams gridParams);

    /// <summary>
    /// Adds a new entity of type <typeparamref name="T"/> to the context.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    T Create(T entity);

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="T"/> in the context.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Deletes an existing entity of type <typeparamref name="T"/> from the context.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);
}
