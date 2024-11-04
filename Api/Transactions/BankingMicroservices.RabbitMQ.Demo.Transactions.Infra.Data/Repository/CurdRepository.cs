using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Repository;

public class CurdRepository<TEntity>(TransactionDbContext context)
    : ICrudRepository<TEntity>
    where TEntity : BaseEntity
{
    protected TransactionDbContext Context => context;
    protected string EntityName => typeof(TEntity).Name.ToUpper();


    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the ID of the added entity.</returns>
    public virtual async Task<Result<int>> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            return Result<int>.Failure(new Error($"ERR_ADD_{EntityName}_NULL", $"The {EntityName} provided for addition is null."));
        }
        await context.AddAsync(entity, cancellationToken);
        var saveResult = await context.SaveChangesAsync(cancellationToken);
        if (saveResult <= 0)
        {
            return Result<int>.Failure(new Error($"ERR_SAVE_{EntityName}_FAILED", $"No {EntityName} records were saved to the database."));
        }
        return Result<int>.Success(entity.Id);
    }

    /// <summary>
    /// Deletes an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    public virtual async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await context.Set<TEntity>().FindAsync([id], cancellationToken);
            if (entity is null)
            {
                return Result.Failure(new Error($"ERR_{EntityName}_NOT_FOUND", $"The {EntityName} with the specified ID was not found."));
            }
            context.Set<TEntity>().Remove(entity);
            var removeResult = await context.SaveChangesAsync(cancellationToken);
            if (removeResult <= 0)
            {
                return Result.Failure(new Error($"ERR_REMOVE_{EntityName}_FAILED", $"No {EntityName} records were removed from the database."));
            }
            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure(new Error($"ERR_CONCURRENCY_{EntityName}_EXCEPTION", $"A concurrency exception occurred while deleting the {EntityName}."));
        }
    }

    /// <summary>
    /// Gets an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the retrieved entity.</returns>
    public virtual async Task<Result<TEntity?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await context.Set<TEntity>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return Result<TEntity?>.Failure(new Error($"ERR_{EntityName}_NOT_FOUND", $"The {EntityName} with the specified ID was not found."));
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
    public virtual async Task<Result<IQueryable<TEntity>>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<IQueryable<TEntity>>.Failure(new Error($"ERR_INVALID_{EntityName}_PAGE_NUMBER", "Page number must be greater than 0."));
        }

        if (pageSize <= 0)
        {
            return Result<IQueryable<TEntity>>.Failure(new Error($"ERR_INVALID_{EntityName}_PAGE_SIZE", "Page size must be greater than 0."));
        }

        var query = context.Set<TEntity>()
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize)
                           .AsQueryable();

        if (!await query.AnyAsync(cancellationToken))
        {
            return Result<IQueryable<TEntity>>.Failure(new Error($"ERR_NO_{EntityName}_RECORDS_FOUND", $"No {EntityName} records were found."));
        }

        return Result<IQueryable<TEntity>>.Success(query);
    }

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation.</returns>
    public virtual async Task<Result> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        if (entity is null)
        {
            return Result.Failure(new Error($"ERR_UPDATE_{EntityName}_NULL", $"The {EntityName} provided for update is null."));
        }

        try
        {
            var existingEntityResult = await GetByIdAsync(entity.Id, cancellationToken);
            if (existingEntityResult.IsFailure || existingEntityResult.Value is null)
            {
                return Result.Failure(new Error($"ERR_{EntityName}_NOT_FOUND", $"The {EntityName} with the specified ID was not found."));
            }
            // Detach the existing entity to avoid tracking conflicts
            context.Entry(existingEntityResult.Value).State = EntityState.Detached;

            entity.CreatedDate = existingEntityResult.Value.CreatedDate;
            entity.UpdatedDate = DateTime.Now;
            context.Set<TEntity>().Update(entity);
            var updateResult = await context.SaveChangesAsync(cancellationToken);
            if (updateResult <= 0)
            {
                return Result.Failure(new Error($"ERR_UPDATE_{EntityName}_FAILED", $"No {EntityName} records were updated in the database."));
            }

            return Result.Success();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Result.Failure(new Error($"ERR_CONCURRENCY_{EntityName}_EXCEPTION", $"A concurrency exception occurred while updating the {EntityName}."));
        }
    }
}
