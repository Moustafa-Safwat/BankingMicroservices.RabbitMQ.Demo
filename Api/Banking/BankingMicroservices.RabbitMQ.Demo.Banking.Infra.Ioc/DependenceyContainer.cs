using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Services;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Mapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.UserQueries;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Services;
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
        services.AddScoped<ICurdService<AddAccountDto, UpdateAccountDto, AccountSearchDto>,
                           CurdService<AddAccountDto, UpdateAccountDto, AccountSearchDto, Account>>();
        services.AddScoped<ICurdService<UserAddDto, UserUpdateDto, UserSearchDto>,
                           CurdService<UserAddDto, UserUpdateDto, UserSearchDto, User>>();
        services.AddScoped<IAccountService, AccountService>();
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

    public static IServiceCollection RegisterRequestValidator(this IServiceCollection services)
    {
        services.AddScoped<IResultValidator<AccountListQuery, IQueryable<AccountSearchDto>>
            , ResultValidator<AccountListQuery, IQueryable<AccountSearchDto>>>();
        services.AddScoped<IResultValidator<UserByAccountIdQuery, UserSearchDto>
            , ResultValidator<UserByAccountIdQuery, UserSearchDto>>();
        services.AddScoped<IResultValidator<AccountQuery, object>
             , ResultValidator<AccountQuery, object>>();
        services.AddScoped<IResultValidator<UserQuery, UserSearchDto>
            , ResultValidator<UserQuery, UserSearchDto>>();
        services.AddScoped<IResultValidator<DeleteUserCommand>
            , ResultValidator<DeleteUserCommand>>();
        services.AddScoped<IResultValidator<UsersQuery, IQueryable<UserSearchDto>>
            , ResultValidator<UsersQuery, IQueryable<UserSearchDto>>>();
        return services;
    }
    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        AutoMapperConfiguration.RegisterMappings();
        return services;
    }
}
