using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public sealed class DeleteAccountCommandHandler(
    IAccountService accountService
    )
    : ICommandHandler<DeleteAccountCommand>
{
    public async Task<Result> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await accountService.DeleteAsync(request.Id, cancellationToken);
        return result.IsSuccess ? Result.Success() : Result.Failures(result.Errors);
    }
}
