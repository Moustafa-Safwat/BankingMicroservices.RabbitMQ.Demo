using BankingMicroservices.RabbitMQ.Demo.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;

public sealed class CreateTransactionEvent : Event
{
    public int TransactionId { get; init; }
    public int FromAccount { get; init; }
    public int ToAccount { get; init; }
    public decimal Amount { get; init; }
    public CreateTransactionEvent()
    {
        Priority = 1;
    }
}
