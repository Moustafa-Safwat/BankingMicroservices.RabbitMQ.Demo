using AutoMapper;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Mapper;

public class AutoMapperConfiguration
{
    /// <summary>
    /// Registers the AutoMapper mappings.
    /// </summary>
    /// <returns>The configured MapperConfiguration.</returns>
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new CommandsAndDtoMapping());
            cfg.AddProfile(new QueriesAndDtoMapping());
            cfg.AddProfile(new EntitiesAndDtos());
        });
    }
}
