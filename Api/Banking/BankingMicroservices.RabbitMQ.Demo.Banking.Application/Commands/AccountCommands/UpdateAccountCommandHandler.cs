using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

/// <summary>
/// Handles the update account command.
/// </summary>
/// <param name="accountService">The account service.</param>
/// <param name="mapper">The mapper.</param>
public class UpdateAccountCommandHandler(
    IAccountService accountService,
    IMapper mapper
    )
    : ICommandHandler<UpdateAccountCommand, bool>
{
    /// <summary>
    /// Handles the update account command.
    /// </summary>
    /// <param name="request">The update account command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result indicating whether the update was successful.</returns>
    public async Task<Result<bool>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountDto = mapper.Map<UpdateAccountCommand, UpdateAccountDto>(request);
        var result = await accountService.UpdateAsync(accountDto, cancellationToken);
        return result.IsSuccess ? Result<bool>.Success(true) : Result<bool>.Failures(result.Errors);
    }
}
