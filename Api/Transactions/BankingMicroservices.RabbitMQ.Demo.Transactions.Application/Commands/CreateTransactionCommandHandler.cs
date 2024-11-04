using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;

public sealed class CreateTransactionCommandHandler(
    ITransactionService transactionService,
    IMapper mapper
    )
    : ICommandHandler<CreateTransactionCommand>
{
    public async Task<Result> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var addTransactionDto = mapper.Map<CreateTransactionCommand, AddTransactionDto>(request);
        var result = await transactionService.AddAsync(addTransactionDto, cancellationToken);
        return result.IsSuccess ? Result.Success() : Result.Failures(result.Errors);
    }
}
