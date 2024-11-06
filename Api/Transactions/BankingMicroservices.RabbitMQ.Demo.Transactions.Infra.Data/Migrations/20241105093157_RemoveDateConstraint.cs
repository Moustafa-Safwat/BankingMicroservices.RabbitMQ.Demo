using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDateConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Transaction_CreatedDate_Valid",
                table: "Transactions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Transaction_UpdatedDate_Valid",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Transaction_CreatedDate_Valid",
                table: "Transactions",
                sql: "[CreatedDate] <= GETDATE()");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transaction_UpdatedDate_Valid",
                table: "Transactions",
                sql: "[UpdatedDate] <= GETDATE()");
        }
    }
}
