using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;

public sealed class UpdateUserCommandHandler(
    IMapper mapper,
    ICurdService<UserAddDto, UserUpdateDto, UserSearchDto> curdService
    )
    : ICommandHandler<UpdateUserCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var userUpdateDto = mapper.Map<UpdateUserCommand, UserUpdateDto>(request);
        var result = await curdService.UpdateAsync(userUpdateDto, cancellationToken);
        return result.IsSuccess
            ? Result<bool>.Success(true)
            : Result<bool>.Failures(result.Errors);
    }
}
