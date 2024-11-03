using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.EntitiesConfigurations;

public sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Transaction> builder)
    {
        throw new NotImplementedException();
    }
}
