using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedAndModifiedDateToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Account",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Account");
        }
    }
}
