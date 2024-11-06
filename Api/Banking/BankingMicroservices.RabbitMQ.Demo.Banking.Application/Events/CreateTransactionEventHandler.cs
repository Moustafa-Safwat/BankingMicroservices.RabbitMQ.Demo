using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Events;

public sealed class CreateTransactionEventHandler(
    IAccountService accountService,
    IEventBus eventBus,
    IConfiguration configuration
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

            await eventBus.PublishAsync(new CreateNotificationEvent
            {
                // Emails will be sent to specified recipient for testing only
                // in real world scenario, the recipient will be the account holder
                Recipient = configuration.GetValue<string>("Emails:Recipient")!,
                Body = $"Transaction {@event.TransactionId} has been rejected at {DateTime.Now}",
                Subject = "Transaction rejected"
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

            await eventBus.PublishAsync(new CreateNotificationEvent
            {
                // Emails will be sent to specified recipient for testing only
                // in real world scenario, the recipient will be the account holder
                Recipient = configuration.GetValue<string>("Emails:Recipient")!,
                Body = $"Transaction {@event.TransactionId} has been completed successfully at {DateTime.Now}",
                Subject = "Transaction completed"
            });

        }
    }
}
