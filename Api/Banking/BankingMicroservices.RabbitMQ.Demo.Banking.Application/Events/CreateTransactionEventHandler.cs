using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Events;

public sealed class CreateTransactionEventHandler(
    IAccountService accountService,
    IEventBus eventBus
    )
    : IEventHandler<CreateTransactionEvent>
{
    public async Task Handel(CreateTransactionEvent @event, CancellationToken cancellationToken)
    {
        var withDrawalResult = await accountService.WithDrawal(@event.FromAccount, @event.Amount, cancellationToken);
        if (withDrawalResult.IsFailure)
        {
            await eventBus.PublishAsync(new ChangeTransactionStatusEvent
            {
                Status = TransactionStatus.Rejected,
                TransactionId = @event.TransactionId
            });
        }
        else
        {   // Success
            await accountService.Deposit(@event.ToAccount, @event.Amount, cancellationToken);
            await eventBus.PublishAsync(new ChangeTransactionStatusEvent
            {
                Status = TransactionStatus.Completed,
                TransactionId = @event.TransactionId
            });

        }
    }
}
