using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;

public interface IAccountService : ICurdService<AddAccountDto, UpdateAccountDto, AccountSearchDto>
{
    Task<Result<User>> GetUserByAccountIdAsync(int accountId, CancellationToken cancellationToken);
}
