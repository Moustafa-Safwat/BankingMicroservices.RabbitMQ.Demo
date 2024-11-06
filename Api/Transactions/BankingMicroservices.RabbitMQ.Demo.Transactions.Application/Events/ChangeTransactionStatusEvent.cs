using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;

public sealed class ChangeTransactionStatusEvent : Event
{
    public int TransactionId { get; init; }
    public TransactionStatus Status { get; set; }
    public ChangeTransactionStatusEvent()
    {
        Priority = 1;
    }
}
