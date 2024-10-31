namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public sealed record UserSearchDto(
    int Id,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime CreatedDate,
    DateTime UpdatedDate,
    byte[] RowVersion
    );
