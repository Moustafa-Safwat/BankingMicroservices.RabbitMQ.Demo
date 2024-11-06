using BankingMicroservices.RabbitMQ.Demo.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BankingMicroservices.RabbitMQ.Demo.Infra.Bus;

/// <summary>
/// Implements an event bus using RabbitMQ for publishing and subscribing to events.
/// </summary>
public sealed class RabbitMqBus(
    IConfiguration configuration,
    IServiceScopeFactory serviceScopeFactory
    ) : IEventBus
{

    private readonly Dictionary<string, List<Type>> handlers = []; // <EventName,HandlerType>
    private readonly List<Type> eventTypes = []; // <EventType>

    /// <summary>
    /// Publishes an event asynchronously to RabbitMQ.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <param name="event">The event to publish.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
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

    /// <summary>
    /// Subscribes to an event asynchronously.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">Thrown when the handler type is already registered for the event.</exception>
    public async Task SubscribeAsync<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>
    {
        var eventName = typeof(TEvent).Name;
        var handlerType = typeof(TEventHandler);

        if (!eventTypes.Contains(typeof(TEvent)))
        {
            eventTypes.Add(typeof(TEvent));
        }
        if (!handlers.TryAdd(eventName, []))
        {
            if (handlers[eventName].Any(h => h == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for '{eventName}'");
            }
        }

        handlers[eventName].Add(handlerType);

        await StartBasicConsume(eventName);
    }

    /// <summary>
    /// Starts consuming messages from RabbitMQ for a specific event.
    /// </summary>
    /// <param name="eventName">The name of the event to consume.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task StartBasicConsume(string eventName)
    {
        var factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMq:HostName"],
            DispatchConsumersAsync = true
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var eventName = ea.RoutingKey;
            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
            try
            {
                await ProcessEventAsync(eventName, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        };
        channel.BasicConsume(queue: eventName,
                             autoAck: true,
                             consumer: consumer);
        await Task.CompletedTask;
    }

    /// <summary>
    /// Processes an event by deserializing the message and invoking the appropriate handlers.
    /// </summary>
    /// <param name="eventName">The name of the event.</param>
    /// <param name="message">The message containing the event data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task ProcessEventAsync(string eventName, string message)
    {
        if (handlers.TryGetValue(eventName, out var handlerTypes) && eventTypes.Any(e => e.Name == eventName))
        {
            var eventType = eventTypes.SingleOrDefault(t => t.Name == eventName);
            var @event = JsonSerializer.Deserialize(message, eventType!) as Event;
            if (@event is not null)
            {
                // Execute the handlers based on the Priority of the event.
                // Priority= 10 will be execute before Priority= 5.
                foreach (var handlerType in handlerTypes.OrderByDescending(h => @event.Priority))
                {
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

                        if (handler == null) continue;
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType!);
                        var result = concreteType?.GetMethod(nameof(IEventHandler<Event>.Handel))?
                           .Invoke(handler, [@event, new CancellationTokenSource().Token]);
                        if (result is null) continue;
                        await (Task)result;
                    }
                };
            }
        }
    }
}
