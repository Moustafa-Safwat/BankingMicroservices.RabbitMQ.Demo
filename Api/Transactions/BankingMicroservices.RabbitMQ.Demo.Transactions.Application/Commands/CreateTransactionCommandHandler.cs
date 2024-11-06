using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;

public sealed class CreateTransactionCommandHandler(
    ITransactionService transactionService,
    IMapper mapper,
    IEventBus eventBus
    )
    : ICommandHandler<CreateTransactionCommand, int>
{
    public async Task<Result<int>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var addTransactionDto = mapper.Map<CreateTransactionCommand, AddTransactionDto>(request);
        var result = await transactionService.AddAsync(addTransactionDto, cancellationToken);
        if (result.IsFailure)
        {
            return Result<int>.Failures(result.Errors);
        }
        var createTransactionEvent = mapper.Map<CreateTransactionCommand, CreateTransactionEvent>(request);
        await eventBus.PublishAsync(createTransactionEvent);
        return Result<int>.Success(result.Value);
    }
}
