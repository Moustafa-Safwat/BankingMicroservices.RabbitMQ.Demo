namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;

public enum TransactionStatus
{
    Pending = 1,
    Completed = 2,
    Rejected = 3
}
