namespace BankingMicroservices.RabbitMQ.Demo.Transaction.Core.Entities;

public enum TransactionStatus
{
    Pending = 1,
    Completed = 2,
    Rejected = 3
}
