using BankingMicroservices.RabbitMQ.Demo.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

public sealed record AccountQuery : IQuery<object>
{
    public int Id { get; init; }
    public bool IncludeUser { get; init; }
}
