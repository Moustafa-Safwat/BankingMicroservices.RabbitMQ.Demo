using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.UserQueries;

public sealed record UsersQuery(
    int PageNumber,
    int PageSize
    )
    : IQuery<IQueryable<UserSearchDto>>;
