using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;

public sealed record SearchTransactionDto : BaseDto
{
    public int FromAccount { get; init; }
    public int ToAccount { get; init; }
    public decimal Amount { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
    public TransactionStatus Status { get; init; }
}
