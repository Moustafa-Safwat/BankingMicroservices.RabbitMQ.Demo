using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public sealed record UpdateAccountDto : BaseDto
{
    public decimal Balance { get; init; }
    public bool IsActive { get; init; }
    public int UserId { get; init; }
    public DateTime UpdatedDate => DateTime.Now;
}
