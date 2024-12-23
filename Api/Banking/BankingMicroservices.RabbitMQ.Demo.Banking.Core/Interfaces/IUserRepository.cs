﻿using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;

public interface IUserRepository : ICrudRepository<User>
{
    Task<Result<IQueryable<Account>>> GetUserAccounts(int userId, CancellationToken cancellationToken);
}
