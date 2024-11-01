using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

/// <summary>
/// Handles account queries by validating the request and retrieving account information.
/// </summary>
/// <param name="mapper">The mapper to map between objects.</param>
/// <param name="accountService">The service to interact with account data.</param>
/// <param name="requestValidator">The validator to validate the query request.</param>
public sealed class AccountQueryHandler(
    IMapper mapper,
    IAccountService accountService,
    IResultValidator<AccountQuery, object> requestValidator
    )
    : IQueryHandler<AccountQuery, object>
{
    /// <summary>
    /// Handles the account query request.
    /// </summary>
    /// <param name="request">The account query request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result containing the account information or errors if the request fails.</returns>
    public async Task<Result<object>> Handle(AccountQuery request, CancellationToken cancellationToken)
    {
        var errors = await requestValidator.QueryValidateAsync(request, cancellationToken);
        if (errors.Any())
        {
            return Result<object>.Failures(errors);
        }

        var accountSearchDto = mapper.Map<AccountQuery, AccountSearchDto>(request);

        if (request.IncludeUser)
        {
            var accountWithUser = await accountService.GetAccountByIdWithUserAsync(accountSearchDto.Id, cancellationToken);
            if (accountWithUser.IsFailure)
            {
                return Result<object>.Failures(accountWithUser.Errors);
            }

            return Result<object>.Success(accountWithUser.Value);
        }
        else
        {
            var account = await accountService.GetByIdAsync(accountSearchDto.Id, cancellationToken);
            if (account.IsFailure)
            {
                return Result<object>.Failures(account.Errors);
            }

            return Result<object>.Success(account.Value!);
        }
    }
}
