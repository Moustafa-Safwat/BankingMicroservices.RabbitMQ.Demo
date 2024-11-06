using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public sealed record DeleteAccountCommand(int Id) : ICommand;
