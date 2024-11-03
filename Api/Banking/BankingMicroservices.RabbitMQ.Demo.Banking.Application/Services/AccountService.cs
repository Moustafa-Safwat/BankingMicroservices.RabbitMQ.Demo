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
    IUserRepository userRepository,
    IMapper mapper
)
    : CurdService<AddAccountDto, UpdateAccountDto, AccountSearchDto, Account>(accountRepository, mapper)
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

    public async Task<Result<AccountWithUserDetailsDto>> GetAccountByIdWithUserAsync(int accountId, CancellationToken cancellationToken)
    {
        var result = await accountRepository.GetAccountByIdWithUserAsync(accountId, cancellationToken);
        return result.IsSuccess
            ? Mapper.Map<Account, AccountWithUserDetailsDto>(result.Value)
            : Result<AccountWithUserDetailsDto>.Failures(result.Errors);
    }

    public async Task<Result<UserSearchDto>> GetUserByAccountIdAsync(int accountId, CancellationToken cancellationToken)
    {
        var result = await accountRepository.GetUserByAccountIdAsync(accountId, cancellationToken);
        return result.IsSuccess ? Result<UserSearchDto>.Success(Mapper.Map<User, UserSearchDto>(result.Value)) : Result<UserSearchDto>.Failures(result.Errors);
    }

    public async Task<Result> Deposit(int accountId, decimal money, CancellationToken cancellationToken)
    {
        // check money is positive value
        if (money <= 0)
        {
            return Result.Failure(new Error("INVALID_AMOUNT", "The deposit amount must be a positive value."));
        }
        // Check if the account exists or not
        var accountResult = await accountRepository.GetByIdAsync(accountId, cancellationToken);
        if (accountResult.IsFailure)
        {
            return Result.Failures(accountResult.Errors);
        }
        var account = accountResult.Value;
        if (account is null)
        {
            return Result.Failure(new Error("ACCOUNT_NOT_FOUND", "The account does not exist."));
        }
        // check if the account is active or not
        if (!account!.IsActive)
        {
            return Result.Failure(new Error("ACCOUNT_INACTIVE", "The account is inactive and cannot perform transactions."));
        }
        // add money to it
        account.Balance += money;
        var updateResult = await accountRepository.UpdateAsync(account, cancellationToken);
        return updateResult.IsSuccess ? Result.Success() : Result.Failures(updateResult.Errors);
    }
    public async Task<Result> WithDrawal(int accountId, decimal money, CancellationToken cancellationToken)
    {
        // check money is positive value
        if (money <= 0)
        {
            return Result.Failure(new Error("INVALID_AMOUNT", "The withdrawal amount must be a positive value."));
        }
        // Check if the account exists or not
        var accountResult = await accountRepository.GetByIdAsync(accountId, cancellationToken);
        if (accountResult.IsFailure)
        {
            return Result.Failures(accountResult.Errors);
        }
        var account = accountResult.Value;
        if (account is null)
        {
            return Result.Failure(new Error("ACCOUNT_NOT_FOUND", "The account does not exist."));
        }
        // check if the account is active or not
        if (!account!.IsActive)
        {
            return Result.Failure(new Error("ACCOUNT_INACTIVE", "The account is inactive and cannot perform transactions."));
        }
        // check if the account has sufficient balance
        if (account.Balance < money)
        {
            return Result.Failure(new Error("INSUFFICIENT_FUNDS", "The account does not have sufficient funds."));
        }
        // subtract money from it
        account.Balance -= money;
        var updateResult = await accountRepository.UpdateAsync(account, cancellationToken);
        return updateResult.IsSuccess ? Result.Success() : Result.Failures(updateResult.Errors);
    }
}
