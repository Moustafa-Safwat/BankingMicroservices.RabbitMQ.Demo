using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Ioc;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Presentation.Configurations;

/// <summary>
/// Provides extension methods for configuring the event bus.
/// </summary>
public static class ConfigEventBus
{
    /// <summary>
    /// Configures the event bus by subscribing to events.
    /// </summary>
    /// <param name="app">The application builder.</param>
    public async static void ConfigureEventBus(this IApplicationBuilder app)
    {
        await app.ApplicationServices.GetRequiredService<SubscribeEvents>().Subscribe();
    }
}
