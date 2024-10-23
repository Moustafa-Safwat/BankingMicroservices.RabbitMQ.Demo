using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;

public interface ICurdService<T>
{
    Task<Result<int>> AddAsync(T entity, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task<Result<T?>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<IQueryable<T>>> GetPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
