using BankingMicroservices.RabbitMQ.Demo.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

public interface IEventHandler
{
}

public interface IEventHandler<TEvent> : IEventHandler where TEvent : Event
{
    Task Handel(TEvent @event);
}
