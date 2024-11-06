using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public sealed record UserAddDto : BaseDto
{
    public string FullName { get; init; }=string.Empty;
    public string Email { get; init; }=string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public DateTime UpdatedDate { get; init; }

    private UserAddDto()
    {
        UpdatedDate = DateTime.Now;
    }
}
