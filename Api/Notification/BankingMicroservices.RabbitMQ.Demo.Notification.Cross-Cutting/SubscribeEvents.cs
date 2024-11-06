using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Notification.Application.Events;

namespace BankingMicroservices.RabbitMQ.Demo.Notification.Infra.Ioc;

public sealed class SubscribeEvents(IEventBus eventBus)
{
    public async Task Subscribe()
    {
        await eventBus.SubscribeAsync<CreateNotificationEvent, CreateNotificationEventHandler>();
    }
}
