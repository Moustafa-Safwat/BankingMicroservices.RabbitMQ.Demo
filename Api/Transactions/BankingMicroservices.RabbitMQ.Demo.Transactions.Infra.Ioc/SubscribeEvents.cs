using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Ioc;

public sealed class SubscribeEvents(IEventBus eventBus)
{
    public async Task Subscribe()
    {
        await eventBus.SubscribeAsync<ChangeTransactionStatusEvent, ChangeTransactionStatusEventHandler>();
    }
}
