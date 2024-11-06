using BankingMicroservices.RabbitMQ.Demo.Transactions.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.EntitiesConfigurations;

public sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        // Set PK
        builder.HasKey(t => t.Id);
        // Set Required fields
        builder.Property(t => t.FromAccount)
            .IsRequired();

        builder.Property(t => t.ToAccount)
            .IsRequired();

        builder.Property(t => t.Amount)
            .IsRequired();

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion<int>();  // Ensure the enum is stored as an integer in the database

        builder.Property(t => t.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Property(t => t.UpdatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");
        // Set Concurrency Token
        builder.Property(t => t.RowVersion)
            .IsConcurrencyToken();
        // Set Check Constraint
        builder.ToTable(t => t.HasCheckConstraint("CK_Transaction_Amount_Positive"
            , $"[{nameof(Transaction.Amount)}] > 0"));
        builder.ToTable(t => t.HasCheckConstraint("CK_Transaction_Accounts_Positive"
            , $"[{nameof(Transaction.FromAccount)}] > 0 AND [{nameof(Transaction.ToAccount)}] > 0"));
    }
}
