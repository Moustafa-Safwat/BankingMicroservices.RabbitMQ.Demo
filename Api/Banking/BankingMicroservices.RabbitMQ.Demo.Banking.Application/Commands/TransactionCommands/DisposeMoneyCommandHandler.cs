using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.TransactionCommands;

internal class DisposeMoneyCommandHandler(
    IAccountService accountService
    ) :
    ICommandHandler<DisposeMoneyCommand>
{
    public async Task<Result> Handle(DisposeMoneyCommand request, CancellationToken cancellationToken)
    {
        return await accountService.Deposit(request.AccountId, request.Amount, cancellationToken);
    }
}
