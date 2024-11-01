using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public sealed record AccountWithUserDetailsDto(
    decimal Balance,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    bool IsActive,
    UserDataWithAccountDto User
    ) : BaseDto;
