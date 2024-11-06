using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;

public sealed record CreateTransactionCommand(
    int FromAccount,
    int ToAccount,
    decimal Amount
    )
    : ICommand<int>;
