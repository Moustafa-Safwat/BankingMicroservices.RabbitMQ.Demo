using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Notification.Application.Events;
using BankingMicroservices.RabbitMQ.Demo.Notification.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Notification.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Notification.Infra.Data.EmailServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankingMicroservices.RabbitMQ.Demo.Notification.Infra.Ioc;

/// <summary>
/// Provides extension methods for registering services in the dependency injection container.
/// </summary>
public static class DependenceyContainer
{
    /// <summary>
    /// Registers application services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to which services are added.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("GmailSettings")); // EmailSettings from UserSecrets
        services.AddScoped<IEmailService, GmailService>();

        services.AddScoped<CreateNotificationEventHandler>();
        services.AddScoped<IEventHandler<CreateNotificationEvent>, CreateNotificationEventHandler>();

        services.AddSingleton<SubscribeEvents>();
        return services;
    }
}
