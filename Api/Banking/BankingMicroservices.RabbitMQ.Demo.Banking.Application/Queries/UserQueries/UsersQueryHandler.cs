using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.UserQueries;

public sealed class UsersQueryHandler(
    ICurdService<UserAddDto, UserUpdateDto, UserSearchDto> curdService,
    IResultValidator<UsersQuery, IQueryable<UserSearchDto>> resultValidator
    )
    : IQueryHandler<UsersQuery, IQueryable<UserSearchDto>>
{
    public async Task<Result<IQueryable<UserSearchDto>>> Handle(UsersQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await resultValidator.QueryValidateAsync(request, cancellationToken);
        if (validationResult.Any())
        {
            return Result<IQueryable<UserSearchDto>>.Failures(validationResult);
        }
        var result = await curdService.GetPaged(request.PageNumber, request.PageSize, cancellationToken);
        return result.IsSuccess
            ? Result<IQueryable<UserSearchDto>>.Success(result.Value)
            : Result<IQueryable<UserSearchDto>>.Failures(result.Errors);
    }
}
