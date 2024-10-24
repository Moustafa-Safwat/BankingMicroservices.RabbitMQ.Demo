using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Context;
using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Repository;

/// <summary>
/// Provides CRUD operations for entities in the database.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public class CurdRepository<TEntity>(AccountDbContext context)
    : ICrudRepository<TEntity>
    where TEntity : BaseEntity
{
    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the ID of the added entity.</returns>
    public async Task<Result<int>> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            return Result<int>.Failure(new Error("ERR_ADD_ENTITY_NULL", "The entity provided for addition is null."));
        }
        await context.AddAsync(entity, cancellationToken);
        var saveResult = await context.SaveChangesAsync(cancellationToken);
        if (saveResult <= 0)
        {
            return Result<int>.Failure(new Error("ERR_SAVE_FAILED", "No records were saved to the database."));
        }
        return Result<int>.Success(entity.Id);
    }

    /// <summary>
    /// Deletes an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await context.Set<TEntity>().FindAsync([id], cancellationToken);
            if (entity is null)
            {
                return Result.Failure(new Error("ERR_ENTITY_NOT_FOUND", "The entity with the specified ID was not found."));
            }
            context.Set<TEntity>().Remove(entity);
            var removeResult = await context.SaveChangesAsync(cancellationToken);
            if (removeResult <= 0)
            {
                return Result.Failure(new Error("ERR_REMOVE_FAILED", "No records were removed from the database."));
            }
            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure(new Error("ERR_CONCURRENCY_EXCEPTION", "A concurrency exception occurred while deleting the entity."));
        }
    }

    /// <summary>
    /// Gets an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the retrieved entity.</returns>
    public async Task<Result<TEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Set<TEntity>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return Result<TEntity?>.Failure(new Error("ERR_ENTITY_NOT_FOUND", "The entity with the specified ID was not found."));
        }
        return Result<TEntity?>.Success(entity);
    }

    /// <summary>
    /// Gets a paged list of entities.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The size of the page.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The result of the operation, including a queryable collection of entities.</returns>
    public async Task<Result<IQueryable<TEntity>>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<IQueryable<TEntity>>.Failure(new Error("ERR_INVALID_PAGE_NUMBER", "Page number must be greater than 0."));
        }

        if (pageSize <= 0)
        {
            return Result<IQueryable<TEntity>>.Failure(new Error("ERR_INVALID_PAGE_SIZE", "Page size must be greater than 0."));
        }

        var query = context.Set<TEntity>()
                           .Skip((pageNumber - 1) * pageSize) // Corrected paging
                           .Take(pageSize)
                           .AsQueryable();

        if (!await query.AnyAsync(cancellationToken))
        {
            return Result<IQueryable<TEntity>>.Failure(new Error("ERR_NO_RECORDS_FOUND", "No records were found."));
        }

        return Result<IQueryable<TEntity>>.Success(query);
    }

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    public async Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            return Result.Failure(new Error("ERR_UPDATE_ENTITY_NULL", "The entity provided for update is null."));
        }

        try
        {
            var existingEntity = await context.Set<TEntity>().FindAsync([entity.Id], cancellationToken);
            if (existingEntity is null)
            {
                return Result.Failure(new Error("ERR_ENTITY_NOT_FOUND", "The entity with the specified ID was not found."));
            }

            context.Entry(existingEntity).CurrentValues.SetValues(entity);
            var updateResult = await context.SaveChangesAsync(cancellationToken);
            if (updateResult <= 0)
            {
                return Result.Failure(new Error("ERR_UPDATE_FAILED", "No records were updated in the database."));
            }

            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure(new Error("ERR_CONCURRENCY_EXCEPTION", "A concurrency exception occurred while updating the entity."));
        }
    }
}
