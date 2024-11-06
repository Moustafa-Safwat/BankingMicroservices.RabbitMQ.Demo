using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.TransactionCommands;

public sealed record WithDrawalMoneyCommand(
    int AccountId,
    decimal Amount
    )
    : ICommand;
