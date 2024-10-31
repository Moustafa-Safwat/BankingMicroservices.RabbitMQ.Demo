using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Application.Services;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Services;

public class AccountService(
    IAccountRepository accountRepository,
    ICrudRepository<Account> crudRepository,
    IUserRepository userRepository,
    IMapper mapper
)
    : CurdService<AddAccountDto, UpdateAccountDto, AccountSearchDto, Account>(crudRepository, mapper)
    , IAccountService
{
    public override async Task<Result<int>> AddAsync(AddAccountDto accountDto, CancellationToken cancellationToken)
    {
        // check if the user is already exists in the db
        var user = await userRepository.GetByIdAsync(accountDto.UserId, cancellationToken);
        if (user.IsFailure)
        {
            return Result<int>.Failures(user.Errors);
        }
        return await base.AddAsync(accountDto, cancellationToken);
    }

    public async Task<Result<UserSearchDto>> GetUserByAccountIdAsync(int accountId, CancellationToken cancellationToken)
    {
        var result = await accountRepository.GetUserByAccountIdAsync(accountId, cancellationToken);
        return result.IsSuccess ? Result<UserSearchDto>.Success(Mapper.Map<User, UserSearchDto>(result.Value)) : Result<UserSearchDto>.Failures(result.Errors);
    }
}
