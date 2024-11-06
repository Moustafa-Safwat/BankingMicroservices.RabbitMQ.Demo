namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

public  sealed record UserDataWithAccountDto(
    int Id,
    string FullName,
    string Email,
    string PhoneNumber
    );
