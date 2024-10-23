using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using MediatR;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

public interface IQueryHandler<TQuery,TResult>
    :IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>
{
}
