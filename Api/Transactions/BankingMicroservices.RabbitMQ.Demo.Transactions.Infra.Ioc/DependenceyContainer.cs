using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Services;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Mapper;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Services;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Ioc;

/// <summary>
/// Provides extension methods to register dependencies for the Transactions module.
/// </summary>
public static class DependenceyContainer
{
    /// <summary>
    /// Registers repository dependencies.
    /// </summary>
    /// <param name="services">The service collection to add the dependencies to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICrudRepository<Transaction>, CurdRepository<Transaction>>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        return services;
    }

    /// <summary>
    /// Registers service dependencies.
    /// </summary>
    /// <param name="services">The service collection to add the dependencies to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICurdService<AddTransactionDto, UpdateTransactionDto, SearchTransactionDto>
            , CurdService<AddTransactionDto, UpdateTransactionDto, SearchTransactionDto, Transaction>>();
        services.AddScoped<ITransactionService, TransactionService>();
        return services;
    }

    public static IServiceCollection RegisterEvents(this IServiceCollection services)
    {
        services.AddScoped<ChangeTransactionStatusEventHandler>();
        services.AddScoped<IEventHandler<ChangeTransactionStatusEvent>, ChangeTransactionStatusEventHandler>();
        services.AddSingleton<SubscribeEvents>();
        return services;
    }

    /// <summary>
    /// Registers AutoMapper configurations.
    /// </summary>
    /// <param name="services">The service collection to add the dependencies to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        AutoMapperConfiguration.RegisterMappings();
        return services;
    }
}
