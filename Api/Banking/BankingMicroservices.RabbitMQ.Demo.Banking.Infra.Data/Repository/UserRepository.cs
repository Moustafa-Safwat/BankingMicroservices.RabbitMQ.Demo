using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Context;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Repository;

/// <summary>
/// Repository for managing <see cref="User"/> entities.
/// </summary>
public class UserRepository(AccountDbContext context) :
    CurdRepository<User>(context),
    IUserRepository
{

    /// <summary>
    /// Gets the accounts associated with a specific user by the user ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of the operation, including the user's accounts.</returns>
    public async Task<Result<IQueryable<Account>>> GetUserAccounts(int userId, CancellationToken cancellationToken)
    {
        // Check if the user exists
        var userResult = await GetByIdAsync(userId, cancellationToken);
        if (userResult.IsFailure)
        {
            return Result<IQueryable<Account>>.Failures(userResult.Errors);
        }

        // Get accounts associated with the user
        var accounts = Context.Accounts.Where(account => account.UserId == userId);
        if (!await accounts.AnyAsync(cancellationToken))
        {
            return Result<IQueryable<Account>>.Failure(new Error("ERR_NO_ACCOUNTS", $"No accounts found for user with Id [{userId}]"));
        }

        return Result<IQueryable<Account>>.Success(accounts);
    }
}
