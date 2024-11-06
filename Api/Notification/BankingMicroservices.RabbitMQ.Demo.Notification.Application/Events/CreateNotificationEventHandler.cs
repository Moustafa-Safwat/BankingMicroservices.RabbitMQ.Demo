using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Notification.Core.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Notification.Application.Events;

public sealed class CreateNotificationEventHandler(
    IEmailService emailService
    )
    : IEventHandler<CreateNotificationEvent>
{
    public async Task Handel(CreateNotificationEvent @event, CancellationToken cancellationToken)
    {
        await emailService.SendEmailAsync(@event.Recipient, @event.Subject, @event.Body, cancellationToken);
    }
}
