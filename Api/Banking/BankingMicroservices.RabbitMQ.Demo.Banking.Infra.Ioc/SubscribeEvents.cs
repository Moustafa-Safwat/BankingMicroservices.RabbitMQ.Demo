using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Events;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Ioc;

public class SubscribeEvents(IEventBus eventBus)
{
    public async Task Subscribe()
    {
        await eventBus.SubscribeAsync<CreateTransactionEvent, CreateTransactionEventHandler>();
    }
}
