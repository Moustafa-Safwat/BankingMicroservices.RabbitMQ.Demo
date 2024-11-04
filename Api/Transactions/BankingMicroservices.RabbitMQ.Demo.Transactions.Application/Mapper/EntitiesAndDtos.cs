using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Mapper;

public sealed class EntitiesAndDtos : Profile
{
    public EntitiesAndDtos()
    {
        CreateMap<Transaction, AddTransactionDto>().ReverseMap();
        CreateMap<Transaction, UpdateTransactionDto>().ReverseMap();
        CreateMap<Transaction, SearchTransactionDto>().ReverseMap();
    }
}
