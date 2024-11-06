using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Mapper;

public sealed class EventsAndCommands:Profile
{
    public EventsAndCommands()
    {
        CreateMap<CreateTransactionEvent,CreateTransactionCommand>().ReverseMap();
    }
}
