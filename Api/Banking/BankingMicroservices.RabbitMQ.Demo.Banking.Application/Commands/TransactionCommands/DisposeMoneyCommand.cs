using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.TransactionCommands;

public sealed record DisposeMoneyCommand(
    int AccountId,
    decimal Amount
    )
    :ICommand;
