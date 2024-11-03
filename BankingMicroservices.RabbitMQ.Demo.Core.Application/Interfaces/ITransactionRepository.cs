using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transaction.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transaction.Core.Interfaces;

public interface ITransactionRepository
{
    Task<Result> ChangeStatusAsync(int id, TransactionStatus transactionStatus, CancellationToken cancellationToken);
}
