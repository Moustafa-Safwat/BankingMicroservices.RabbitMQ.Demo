using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public sealed record CreateAccountCommand : ICommand<int>
{
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
    public int UserId { get; set; }

}
