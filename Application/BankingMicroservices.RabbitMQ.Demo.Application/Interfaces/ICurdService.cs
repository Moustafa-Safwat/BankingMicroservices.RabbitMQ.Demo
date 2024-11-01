using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;

public interface ICurdService<TAddDto, TUpdateDto, TSearchDto>
{
    Task<Result<int>> AddAsync(TAddDto entity, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(TUpdateDto entity, CancellationToken cancellationToken);
    Task<Result<TSearchDto?>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<Result<IQueryable<TSearchDto>>> GetPaged(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
}
