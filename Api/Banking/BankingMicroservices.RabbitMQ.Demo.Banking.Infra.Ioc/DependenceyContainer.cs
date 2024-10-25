using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Repository;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Ioc;

public static class DependenceyContainer
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {

        return services;
    }

    /// <summary>
    /// Registers the repository services with the dependency injection container.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    /// <returns>The IServiceCollection with the registered services.</returns>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICrudRepository<Account>, CurdRepository<Account>>();
        services.AddScoped<ICrudRepository<User>, CurdRepository<User>>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
