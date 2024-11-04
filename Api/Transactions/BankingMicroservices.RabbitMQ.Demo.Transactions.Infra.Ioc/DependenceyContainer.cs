using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Ioc;

public static class DependenceyContainer
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICrudRepository<Transaction>, CurdRepository<Transaction>>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        return services;
    }
}
