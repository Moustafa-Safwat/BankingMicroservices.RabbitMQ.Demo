using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init_Transactions_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromAccount = table.Column<int>(type: "int", nullable: false),
                    ToAccount = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.CheckConstraint("CK_Transaction_Accounts_Positive", "[FromAccount] > 0 AND [ToAccount] > 0");
                    table.CheckConstraint("CK_Transaction_Amount_Positive", "[Amount] > 0");
                    table.CheckConstraint("CK_Transaction_CreatedDate_Valid", "[CreatedDate] <= GETDATE()");
                    table.CheckConstraint("CK_Transaction_UpdatedDate_Valid", "[UpdatedDate] <= GETDATE()");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
