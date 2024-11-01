using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Interfaces;

public interface IAccountService : ICurdService<AddAccountDto, UpdateAccountDto, AccountSearchDto>
{
    Task<Result<UserSearchDto>> GetUserByAccountIdAsync(int accountId, CancellationToken cancellationToken);
    Task<Result<AccountWithUserDetailsDto>> GetAccountByIdWithUserAsync(int accountId, CancellationToken cancellationToken);
}
