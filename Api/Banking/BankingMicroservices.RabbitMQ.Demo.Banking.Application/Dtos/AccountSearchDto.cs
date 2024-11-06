using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public sealed record AccountSearchDto : BaseDto
{
    public decimal Balance { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
    public bool IsActive { get; init; }
    public int UserId { get; set; }
}
