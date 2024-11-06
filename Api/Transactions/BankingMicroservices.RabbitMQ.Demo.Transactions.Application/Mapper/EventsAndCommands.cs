using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Events;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Mapper;

public sealed class EventsAndCommands:Profile
{
    public EventsAndCommands()
    {
        CreateMap<CreateTransactionEvent, CreateTransactionCommand>().ReverseMap()
            .ForMember(dest => dest.TransactionId, opt => opt.MapFrom((src, dest, destMember, context) =>
                context.Items[nameof(CreateTransactionEvent.TransactionId)]))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => 1)); // Set default priority
    }
}
