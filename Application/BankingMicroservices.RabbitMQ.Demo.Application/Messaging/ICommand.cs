using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using MediatR;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}
