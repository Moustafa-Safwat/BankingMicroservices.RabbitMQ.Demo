using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public class CreateAccountCommandHandler(
    IMapper mapper,
    IAccountService accountService
    )
    : ICommandHandler<CreateAccountCommand, int>
{
    public async Task<Result<int>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var addAccountDto = mapper.Map<CreateAccountCommand, AddAccountDto>(request);
        var result =await accountService.AddAsync(addAccountDto, cancellationToken);
        if (result.IsSuccess)
        {
            return result;
        }
        return Result<int>.Failures(result.Errors);
    }
}
