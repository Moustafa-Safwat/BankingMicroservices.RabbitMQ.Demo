using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Events;

public sealed class CreateTransactionEventHandler(
    IAccountService accountService
    )
    : IEventHandler<CreateTransactionEvent>
{
    public async Task Handel(CreateTransactionEvent @event, CancellationToken cancellationToken)
    {
        var withDrawalResult = await accountService.WithDrawal(@event.FromAccount, @event.Amount, cancellationToken);
        if (withDrawalResult.IsFailure)
        {
            return;
        }
        await accountService.Deposit(@event.ToAccount, @event.Amount, CancellationToken.None);
    }
}
