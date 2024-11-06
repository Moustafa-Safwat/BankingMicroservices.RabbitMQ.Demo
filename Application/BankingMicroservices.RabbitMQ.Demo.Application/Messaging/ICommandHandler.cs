using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using MediatR;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResult>
    : IRequestHandler<TCommand, Result<TResult>>
    where TCommand : ICommand<TResult>
{
}