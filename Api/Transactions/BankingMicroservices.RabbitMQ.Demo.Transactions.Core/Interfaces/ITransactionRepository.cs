using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Interfaces;

public interface ITransactionRepository : ICrudRepository<Transaction>
{
    Task<Result> ChangeStatusAsync(int id, TransactionStatus transactionStatus, CancellationToken cancellationToken);
}
