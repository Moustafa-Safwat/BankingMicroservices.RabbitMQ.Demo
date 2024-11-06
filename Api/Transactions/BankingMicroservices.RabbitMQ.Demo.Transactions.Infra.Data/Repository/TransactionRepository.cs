using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Context;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Repository;

public sealed class TransactionRepository(TransactionDbContext context)
    : CurdRepository<Transaction>(context)
    , ITransactionRepository
{
    public override Task<Result<int>> AddAsync(Transaction entity, CancellationToken cancellationToken)
    {
        entity.Status = TransactionStatus.Pending;
        return base.AddAsync(entity, cancellationToken);
    }

    public async Task<Result> ChangeStatusAsync(int id, TransactionStatus transactionStatus, CancellationToken cancellationToken)
    {
        var transactionResult = await GetByIdAsync(id, cancellationToken);
        if (transactionResult.IsFailure)
        {
            return Result.Failures(transactionResult.Errors);
        }
        var transaction = transactionResult.Value;
        if (transaction is null)
        {
            return Result.Failure(new ("TRANSACTION_NOT_FOUND", "The transaction with the specified ID was not found."));
        }
        transaction.Status = transactionStatus;
        return await UpdateAsync(transaction, cancellationToken);
    }

}
