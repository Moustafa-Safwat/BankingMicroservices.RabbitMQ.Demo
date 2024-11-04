using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Mapper;

public sealed class CommandsAndDtoMapping : Profile
{
    public CommandsAndDtoMapping()
    {
        CreateMap<CreateTransactionCommand, AddTransactionDto>();
    }
}
