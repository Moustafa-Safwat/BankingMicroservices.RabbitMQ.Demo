using BankingMicroservices.RabbitMQ.Demo.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;

public sealed class CreateTransactionEvent : Event
{
    public int FromAccount { get; set; }
    public int ToAccount { get; set; }
    public decimal Amount { get; set; }
    public CreateTransactionEvent()
    {
        Priority = 1;
    }
}
