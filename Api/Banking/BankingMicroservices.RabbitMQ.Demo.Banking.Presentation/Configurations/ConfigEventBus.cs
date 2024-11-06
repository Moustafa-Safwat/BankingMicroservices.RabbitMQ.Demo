using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Ioc;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Presentation.Configurations;

public static class ConfigEventBus
{
    public async static void ConfigureEventBus(this IApplicationBuilder app)
    {
        await app.ApplicationServices.GetRequiredService<SubscribeEvents>().Subscribe();
    }
}
