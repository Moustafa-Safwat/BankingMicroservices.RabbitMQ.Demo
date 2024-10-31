using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

public sealed class UserByAccountIdQueryHandler(
    IAccountService accountService,
    IResultValidator<UserByAccountIdQuery, UserSearchDto> requestValidator
    )
    : IQueryHandler<UserByAccountIdQuery, UserSearchDto>
{
    public async Task<Result<UserSearchDto>> Handle(UserByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var errors = await requestValidator.QueryValidateAsync(request, cancellationToken);
        if (errors.Any())
        {
            return Result<UserSearchDto>.Failures(errors);
        }
        var result = await accountService.GetUserByAccountIdAsync(request.Id, cancellationToken);
        return result.IsSuccess ? Result<UserSearchDto>.Success(result.Value) : Result<UserSearchDto>.Failures(result.Errors);
    }
}
