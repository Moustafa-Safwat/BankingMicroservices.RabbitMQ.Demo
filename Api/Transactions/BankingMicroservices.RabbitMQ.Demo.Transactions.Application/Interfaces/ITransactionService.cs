using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Interfaces;

public interface ITransactionService : ICurdService<AddTransactionDto, UpdateTransactionDto, SearchTransactionDto>
{
    Task<Result> ChangeStatusAsync(int id, TransactionStatus transactionStatus, CancellationToken cancellationToken);
}
