using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Context;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Repository;

/// <summary>
/// Repository for managing <see cref="Account"/> entities.
/// </summary>
public class AccountRepository(AccountDbContext context)
    : CurdRepository<Account>(context)
    , IAccountRepository
{
    public async Task<Result<Account>> GetAccountByIdWithUserAsync(int accountId, CancellationToken cancellationToken)
    {
        var account = await Context.Set<Account>()
       .Include(a => a.User)
       .FirstOrDefaultAsync(a => a.Id == accountId, cancellationToken);

        if (account == null)
        {
            return Result<Account>.Failure(new Error($"ERR_ACCOUNT_NOT_FOUND", $"The account with the specified ID was not found."));
        }
        return Result<Account>.Success(account);
    }

    /// <summary>
    /// Gets the user associated with a specific account by the account ID.
    /// </summary>
    /// <param name="accountId">The ID of the account.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the associated user.</returns>
    public async Task<Result<User>> GetUserByAccountIdAsync(int accountId, CancellationToken cancellationToken)
    {
        // Get account by Id
        var result = await GetByIdAsync(accountId, cancellationToken);
        if (result.IsFailure)
        {
            return Result<User>.Failures(result.Errors);
        }

        // Include user and filter by account Id
        var users = Context.Accounts.Include(account => account.User)
           .Where(account => account.Id == accountId);
        int usersCount = await users.CountAsync(cancellationToken);
        switch (usersCount)
        {
            case 0:
                return Result<User>.Failure(new Error("ERR_USER_NOT_FOUND", $"No user attached to account with Id [{accountId}]"));
            case 1:
                var user = users.FirstAsync(cancellationToken).Result.User;
                return Result<User>.Success(user);
            default:
                return Result<User>.Failure(new Error("ERR_MULTIPLE_USERS", $"Account with Id [{accountId}] has more than one user attached"));
        }
    }
}
