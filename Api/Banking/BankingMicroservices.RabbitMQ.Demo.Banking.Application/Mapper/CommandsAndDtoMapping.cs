using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Mapper;

public sealed class CommandsAndDtoMapping : Profile
{
    public CommandsAndDtoMapping()
    {
        CreateMap<CreateAccountCommand, AddAccountDto>();
    }
}
