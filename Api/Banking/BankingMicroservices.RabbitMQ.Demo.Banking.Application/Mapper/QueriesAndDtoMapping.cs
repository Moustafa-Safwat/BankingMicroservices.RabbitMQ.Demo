using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Mapper;

public sealed class QueriesAndDtoMapping:Profile
{
    public QueriesAndDtoMapping()
    {
        CreateMap<AccountQuery, AccountSearchDto>();
    }
}
