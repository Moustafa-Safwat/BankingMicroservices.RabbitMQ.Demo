using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.UserQueries;

public class UserQueryHandler(
    ICurdService<UserAddDto, UserUpdateDto, UserSearchDto> curdService,
    IResultValidator<UserQuery, UserSearchDto> requestValidator
    )
    : IQueryHandler<UserQuery, UserSearchDto>
{
    public async Task<Result<UserSearchDto>> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await requestValidator.QueryValidateAsync(request, cancellationToken);
        if (validationResult.Any())
        {
            return Result<UserSearchDto>.Failures(validationResult);
        }
        var result = await curdService.GetByIdAsync(request.Id, cancellationToken);
        return result.IsSuccess ?
            Result<UserSearchDto>.Success(result.Value!)
            : Result<UserSearchDto>.Failures(result.Errors);
    }
}
