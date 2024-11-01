using AutoMapper;
using BankingMicroservices.RabbitMQ.Demo.Banking.Application.Dtos;
using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Mapper;

public sealed class EntitiesAndDtos : Profile
{
    public EntitiesAndDtos()
    {
        CreateMap<Account, AccountSearchDto>().ReverseMap();
        CreateMap<Account, AddAccountDto>().ReverseMap();
        CreateMap<Account, UpdateAccountDto>().ReverseMap();
        CreateMap<Account, AccountWithUserDetailsDto>().ReverseMap();
        CreateMap<User, UserSearchDto>().ReverseMap();
        CreateMap<User, UserDataWithAccountDto>().ReverseMap();
        CreateMap<User, UserDataWithAccountDto>().ReverseMap();
        CreateMap<User, UserAddDto>().ReverseMap();
    }
}
