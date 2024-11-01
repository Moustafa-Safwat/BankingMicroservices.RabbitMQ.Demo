using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;

public class CreateUserCommandHandler(
    ICurdService<UserAddDto, UserUpdateDto, UserSearchDto> curdService,
    IMapper mapper
    )
    : ICommandHandler<CreateUserCommand, int>
{
    public async Task<Result<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userAddDto = mapper.Map<CreateUserCommand, UserAddDto>(request);
        var result = await curdService.AddAsync(userAddDto, cancellationToken);
        return result.IsSuccess
            ? Result<int>.Success(result.Value)
            : Result<int>.Failures(result.Errors);
    }
}
