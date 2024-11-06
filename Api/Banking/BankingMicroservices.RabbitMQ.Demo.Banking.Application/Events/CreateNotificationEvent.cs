using BankingMicroservices.RabbitMQ.Demo.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Events;

/// <summary>
/// Represents an event to create a notification.
/// </summary>
public sealed class CreateNotificationEvent : Event
{
    /// <summary>
    /// Gets or initializes the body of the notification.
    /// </summary>
    public string Body { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes the subject of the notification.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes the recipient of the notification.
    /// </summary>
    public string Recipient { get; init; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateNotificationEvent"/> class with a default priority of 1.
    /// </summary>
    public CreateNotificationEvent()
    {
        Priority = 1;
    }
}
