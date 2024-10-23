using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

/// <summary>
/// Defines a generic repository interface for CRUD operations.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface ICrudRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the ID of the added entity.</returns>
    Task<Result<int>> AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    Task<Result> UpdateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Gets an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the retrieved entity.</returns>
    Task<Result<T?>> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Gets a paged list of entities.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The result of the operation, including a queryable collection of entities.</returns>
    Task<Result<IQueryable<T>>> GetPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
