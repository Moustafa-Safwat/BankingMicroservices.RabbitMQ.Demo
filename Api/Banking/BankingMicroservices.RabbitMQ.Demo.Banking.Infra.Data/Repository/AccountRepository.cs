using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Context;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Repository;

public class AccountRepository(AccountDbContext context)
    : CurdRepository<Account>(context)
    , IAccountRepository
{

}
