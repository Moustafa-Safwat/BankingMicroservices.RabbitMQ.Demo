using BankingMicroservices.RabbitMQ.Demo.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

/// <summary>
/// Defines an interface for an event bus to publish and subscribe to events.
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// Publishes an event asynchronously.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <param name="event">The event to publish.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PublishAsync<TEvent>(TEvent @event) where TEvent : Event;

    /// <summary>
    /// Subscribes to an event asynchronously.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <typeparam name="TEventHandler">The type of the event handler.</typeparam>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SubscribeAsync<TEvent, TEventHandler>()
        where TEvent : Event
        where TEventHandler : IEventHandler<TEvent>;
}
