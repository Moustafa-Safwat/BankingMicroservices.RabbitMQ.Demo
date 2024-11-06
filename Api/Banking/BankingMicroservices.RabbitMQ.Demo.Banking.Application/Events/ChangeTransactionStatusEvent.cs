using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Events;

public sealed class ChangeTransactionStatusEvent : Event
{
    public int TransactionId { get; init; }
    public TransactionStatus Status { get; set; }
    public ChangeTransactionStatusEvent()
    {
        Priority = 1;
    }
}
