using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace BankingMicroservices.RabbitMQ.Demo.Infra.Ioc;

public static class DependenceyContainer
{
    public static IServiceCollection RegisterEventBus(this IServiceCollection services)
    {
        services.AddSingleton<IEventBus, RabbitMqBus>();
        return services;
    }
}
