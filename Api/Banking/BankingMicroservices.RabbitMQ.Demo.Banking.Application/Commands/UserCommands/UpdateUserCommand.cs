using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using System.Windows.Input;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;

public sealed class UpdateUserCommand : ICommand<bool>
{
    public int Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public byte[] RowVersion { get; init; } = [];
}
