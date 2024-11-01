using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;

public sealed record CreateUserCommand : ICommand<int>
{
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
}
