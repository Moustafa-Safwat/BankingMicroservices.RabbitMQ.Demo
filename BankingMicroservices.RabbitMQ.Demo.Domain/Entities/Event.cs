namespace BankingMicroservices.RabbitMQ.Demo.Core.Entities;

public abstract class Event
{
    public int Priority { get; protected set; }
}
