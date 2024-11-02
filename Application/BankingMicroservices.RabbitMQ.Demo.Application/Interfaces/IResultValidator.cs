using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;

public interface IResultValidator<TRequest, TQuery>
    where TRequest : IQuery<TQuery>
{
    Task<IReadOnlyList<Error>> QueryValidateAsync(TRequest query, CancellationToken cancellationToken);
}

public interface IResultValidator<TRequest>
    where TRequest : ICommand
{
    Task<IReadOnlyList<Error>> CommandValidateAsync(TRequest query, CancellationToken cancellationToken);
}

