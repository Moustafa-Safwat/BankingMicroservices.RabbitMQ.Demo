using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public sealed record UpdateAccountCommand : ICommand<bool>
{
    public int Id { get; init; }
    public decimal Balance { get; init; }
    public bool IsActive { get; init; }
    public int UserId { get; init; }
    public byte[] RowVersion { get; init; } = [];
}
