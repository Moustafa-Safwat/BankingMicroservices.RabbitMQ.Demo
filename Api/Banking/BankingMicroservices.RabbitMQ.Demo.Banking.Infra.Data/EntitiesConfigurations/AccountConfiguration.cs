using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.EntitiesConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // Set PK
        builder.HasKey(account => account.Id);
        // SetOneToMany Relationship
        builder.HasOne(account => account.User)
               .WithMany(user => user.Accounts)
               .HasForeignKey(account => account.UserId);
        // Set Required fields
        builder.Property(account => account.Balance).IsRequired();
        builder.Property(account => account.CreatedDate).IsRequired();
        builder.Property(account => account.IsActive).IsRequired();
        // Set Check Constraint
        builder.ToTable(account => account.HasCheckConstraint("CK_Account_Balance_Positive"
            , $"[{nameof(Account.Balance)}] > 0"));
        // Set Concurrency Token
        builder.Property(user => user.RowVersion)
               .IsConcurrencyToken();
    }
}
