using AutoMapper;
using AutoMapper.QueryableExtensions;
using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Services;

/// <summary>
/// Represents a service for performing CRUD operations on entities.
/// </summary>
/// <typeparam name="TDto">The type of entity.</typeparam>
public class CurdService<TDto, TEntity>(
    ICrudRepository<TEntity> repository,
    IMapper mapper
    )
    : ICurdService<TDto>
    where TDto : BaseDto, new()
    where TEntity : BaseEntity, new()
{
    protected IMapper Mapper => mapper;

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="dto">The entity to add.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the entity was added successfully.</returns>
    public virtual async Task<Result<int>> AddAsync(TDto dto, CancellationToken cancellationToken)
    {
        var mappedEntity = mapper.Map<TDto, TEntity>(dto);
        return await repository.AddAsync(mappedEntity, cancellationToken);
    }

    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the entity was deleted successfully.</returns>
    public virtual async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        return await repository.DeleteAsync(id, cancellationToken);
    }

    /// <summary>
    /// Gets an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the entity with the specified ID, or null if not found.</returns>
    public virtual async Task<Result<TDto?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        return mapper.Map<TEntity, TDto>(entity.Value!);
    }

    /// <summary>
    /// Gets a paged list of entities.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A queryable collection of entities representing the specified page.</returns>
    public virtual async Task<Result<IQueryable<TDto>>> GetPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await repository.GetPaged(pageNumber, pageSize, cancellationToken);
         return Result<IQueryable<TDto>>.Success(result.Value.ProjectTo<TDto>(mapper.ConfigurationProvider));
    }

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the entity was updated successfully.</returns>
    public virtual async Task<Result> UpdateAsync(TDto entity, CancellationToken cancellationToken)
    {
        var mappedEntity = mapper.Map<TDto, TEntity>(entity);
        return await repository.UpdateAsync(mappedEntity, cancellationToken);
    }
}
