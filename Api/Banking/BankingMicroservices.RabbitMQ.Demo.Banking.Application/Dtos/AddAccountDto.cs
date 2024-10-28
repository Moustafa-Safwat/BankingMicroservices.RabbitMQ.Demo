using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public sealed record AddAccountDto : BaseDto
{
    public decimal Balance { get; init; }
    public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
    public bool IsActive { get; init; }
    public int UserId { get; init; }
}
