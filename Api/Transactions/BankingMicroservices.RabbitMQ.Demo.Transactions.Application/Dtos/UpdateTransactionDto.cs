using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;

public sealed record UpdateTransactionDto : BaseDto
{
    public int FromAccount { get; init; }
    public int ToAccount { get; init; }
    public decimal Amount { get; init; }
    public TransactionStatus Status { get; init; }
    public DateTime UpdatedDate { get; init; }

    private UpdateTransactionDto()
    {
        UpdatedDate = DateTime.Now;
    }
}
