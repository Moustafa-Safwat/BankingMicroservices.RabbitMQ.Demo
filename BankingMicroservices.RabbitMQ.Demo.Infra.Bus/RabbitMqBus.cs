using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BankingMicroservices.RabbitMQ.Demo.Infra.Bus;

public sealed class RabbitMqBus(IConfiguration configuration) : IEventBus
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event
    {
        var factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMq:HostName"],
        };
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            var message = JsonSerializer.Serialize(@event);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: typeof(TEvent).Name,
                                 basicProperties: null,
                                 body: body);
        }
        await Task.CompletedTask;
    }

    public Task SubscribeAsync<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>
    {
        throw new NotImplementedException();
    }
}
