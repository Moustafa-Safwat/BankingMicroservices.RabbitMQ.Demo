using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Services;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Services;

public sealed class TransactionService(
    ITransactionRepository transactionRepository,
    IMapper mapper
    )
    : CurdService<AddTransactionDto, UpdateTransactionDto, SearchTransactionDto, Transaction>(transactionRepository, mapper)
    , ITransactionService
{
    public async Task<Result> ChangeStatusAsync(int id, TransactionStatus transactionStatus, CancellationToken cancellationToken)
    {
        var result = await transactionRepository.ChangeStatusAsync(id, transactionStatus, cancellationToken);
        return result.IsSuccess ? Result.Success() : Result.Failures(result.Errors);
    }
}
