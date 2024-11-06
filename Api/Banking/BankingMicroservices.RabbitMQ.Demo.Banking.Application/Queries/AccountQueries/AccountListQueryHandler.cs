using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

public sealed class AccountListQueryHandler(
    IAccountService accountService,
    IResultValidator<AccountListQuery, IQueryable<AccountSearchDto>> validator
    )
    : IQueryHandler<AccountListQuery, IQueryable<AccountSearchDto>>
{
    public async Task<Result<IQueryable<AccountSearchDto>>> Handle(AccountListQuery request, CancellationToken cancellationToken)
    {
        var errors = await validator.QueryValidateAsync(request, cancellationToken);
        if (errors.Any())
        {
            return Result<IQueryable<AccountSearchDto>>.Failures(errors);
        }
        var result = await accountService.GetPaged(request.PageNumber, request.PageSize, cancellationToken);
        return result.IsSuccess
            ? Result<IQueryable<AccountSearchDto>>.Success(result.Value)
            : Result<IQueryable<AccountSearchDto>>.Failures(result.Errors);
    }
}
