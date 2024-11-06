using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;

public sealed class ChangeTransactionStatusEventHandler(
    ITransactionService transactionService
    )
    : IEventHandler<ChangeTransactionStatusEvent>
{
    public async Task Handel(ChangeTransactionStatusEvent @event, CancellationToken cancellationToken)
    {
        await transactionService.ChangeStatusAsync(@event.TransactionId, @event.Status, cancellationToken);
    }
}
