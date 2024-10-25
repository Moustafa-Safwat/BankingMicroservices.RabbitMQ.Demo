using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;

public interface IAccountRepository : ICrudRepository<Account>
{
    Task<Result<User>> GetUserByAccountIdAsync(int accountId, CancellationToken cancellationToken);
}
