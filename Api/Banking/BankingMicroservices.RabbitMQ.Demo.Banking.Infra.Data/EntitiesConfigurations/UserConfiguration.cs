using BankingMicroservices.RabbitMQ.Demo.Banking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Set PK
        builder.HasKey(user => user.Id);
        // SetOneToMany Relationship
        builder.HasMany(user => user.Accounts)
            .WithOne(account => account.User)
            .HasForeignKey(account => account.UserId);
        // Set Required fields
        builder.Property(user => user.FullName).IsRequired();
        builder.Property(user => user.Email).IsRequired();
        builder.Property(user => user.PhoneNumber).IsRequired();
        builder.Property(account => account.CreatedDate)
             .IsRequired()
             .HasDefaultValueSql("GETDATE()")
             .ValueGeneratedOnAdd();
        builder.Property(account => account.UpdatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");
        // Set Check Constraint
        builder.ToTable(user => user.HasCheckConstraint("CK_User_Email_Format"
            , $"[{nameof(User.Email)}] LIKE '%_@__%.__%'"));
        builder.ToTable(user => user.HasCheckConstraint("CK_User_CreatedDate"
            , $"[{nameof(User.CreatedDate)}] <= GETDATE()"));
        builder.ToTable(user => user.HasCheckConstraint("CK_User_FullName_Length"
            , $"LEN([{nameof(User.FullName)}]) <= 100"));
        // Set Concurrency Token
        builder.Property(user => user.RowVersion)
            .IsConcurrencyToken();
    }
}
