using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using MediatR;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

public interface IQuery<TResult>
    : IRequest<Result<TResult>>
{
}
