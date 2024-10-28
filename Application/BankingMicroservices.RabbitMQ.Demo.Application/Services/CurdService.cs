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
public class CurdService<TAddDto, TUpdateDto, TSearchDto, TEntity>(
    ICrudRepository<TEntity> repository,
    IMapper mapper
    )
    : ICurdService<TAddDto, TUpdateDto, TSearchDto>
    where TAddDto : BaseDto, new()
    where TUpdateDto : BaseDto, new()
    where TSearchDto : BaseDto, new()
    where TEntity : BaseEntity, new()
{
    protected IMapper Mapper => mapper;

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="dto">The entity to add.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the entity was added successfully.</returns>
    public virtual async Task<Result<int>> AddAsync(TAddDto dto, CancellationToken cancellationToken)
    {
        var mappedEntity = mapper.Map<TAddDto, TEntity>(dto);
        var result = await repository.AddAsync(mappedEntity, cancellationToken);
        return result.IsSuccess ? Result<int>.Success(result.Value) : Result<int>.Failures(result.Errors);
    }

    /// <summary>
    /// Deletes an entity asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the entity was deleted successfully.</returns>
    public virtual async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteAsync(id, cancellationToken);
        return result.IsSuccess ? Result.Success() : Result.Failures(result.Errors);
    }

    /// <summary>
    /// Gets an entity by its ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the entity with the specified ID, or null if not found.</returns>
    public virtual async Task<Result<TSearchDto?>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken);
        if (entity.IsSuccess)
        {
            return Result<TSearchDto?>.Success(mapper.Map<TEntity, TSearchDto>(entity.Value!));
        }
        return Result<TSearchDto?>.Failures(entity.Errors);
    }

    /// <summary>
    /// Gets a paged list of entities.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>A queryable collection of entities representing the specified page.</returns>
    public virtual async Task<Result<IQueryable<TSearchDto>>> GetPaged(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var result = await repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        return result.IsSuccess ? Result<IQueryable<TSearchDto>>.Success(result.Value.ProjectTo<TSearchDto>(mapper.ConfigurationProvider)) 
            : Result<IQueryable<TSearchDto>>.Failures(result.Errors);
    }

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating whether the entity was updated successfully.</returns>
    public virtual async Task<Result> UpdateAsync(TUpdateDto entity, CancellationToken cancellationToken)
    {
        var mappedEntity = mapper.Map<TUpdateDto, TEntity>(entity);
        var result = await repository.UpdateAsync(mappedEntity, cancellationToken);
        return result.IsSuccess ? Result.Success() : Result.Failures(result.Errors);
    }
}
