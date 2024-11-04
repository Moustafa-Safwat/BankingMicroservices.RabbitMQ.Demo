using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Context;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Ioc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext
builder.Services.AddDbContext<TransactionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransactionsDbCS"));
});

// Register Servces and Repositories
builder.Services.RegisterRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
