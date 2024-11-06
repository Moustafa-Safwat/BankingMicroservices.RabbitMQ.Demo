using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;
public sealed class DeleteUserCommandHandler(
    ICurdService<UserAddDto, UserUpdateDto, UserSearchDto> curdService,
    IResultValidator<DeleteUserCommand> resultValidator
    )
    : ICommandHandler<DeleteUserCommand>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var validation = await resultValidator.CommandValidateAsync(request, cancellationToken);
        if (validation.Any())
        {
            return Result.Failures(validation);
        }
        var result = await curdService.DeleteAsync(request.Id, cancellationToken);
        return result.IsSuccess
            ? Result.Success()
            : Result.Failures(result.Errors);
    }
}
