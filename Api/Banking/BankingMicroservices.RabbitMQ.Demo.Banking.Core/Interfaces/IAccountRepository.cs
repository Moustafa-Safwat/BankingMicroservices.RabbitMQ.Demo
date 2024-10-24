using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using BankingMicroservices.RabbitMQ.Demo.Core.Interfaces;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Core.Interfaces;

public interface IAccountRepository : ICrudRepository<Account>
{
}
