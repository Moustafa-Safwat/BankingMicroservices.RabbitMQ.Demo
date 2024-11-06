using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.TransactionCommands;

public sealed class WithDrawalMoneyCommandHandler(
    IAccountService accountService
    )
    : ICommandHandler<WithDrawalMoneyCommand>
{
    public async Task<Result> Handle(WithDrawalMoneyCommand request, CancellationToken cancellationToken)
    {
        return await accountService.WithDrawal(request.AccountId, request.Amount, cancellationToken);
    }
}
