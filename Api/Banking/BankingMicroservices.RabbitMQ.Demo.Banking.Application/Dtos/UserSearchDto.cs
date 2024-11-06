using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public sealed record UserSearchDto(
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime CreatedDate,
    DateTime UpdatedDate
    ):BaseDto;
