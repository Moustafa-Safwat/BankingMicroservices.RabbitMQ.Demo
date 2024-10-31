using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

public sealed class AccountQueryHandler(
    IMapper mapper,
    IAccountService accountService,
    IResultValidator<AccountQuery, AccountSearchDto> reuqsestValidator
    )
    : IQueryHandler<AccountQuery, AccountSearchDto>
{
    public async Task<Result<AccountSearchDto>> Handle(AccountQuery request, CancellationToken cancellationToken)

    {
        var errors = await reuqsestValidator.QueryValidateAsync(request, cancellationToken);
        if (errors.Any())
        {
            return Result<AccountSearchDto>.Failures(errors);
        }
        var accountSearchDto = mapper.Map<AccountQuery, AccountSearchDto>(request);
        var account = await accountService.GetByIdAsync(accountSearchDto.Id, cancellationToken);
        if (account.IsFailure)
        {
            return Result<AccountSearchDto>.Failures(account.Errors);
        }
        return Result<AccountSearchDto>.Success(account.Value!);
    }
}
