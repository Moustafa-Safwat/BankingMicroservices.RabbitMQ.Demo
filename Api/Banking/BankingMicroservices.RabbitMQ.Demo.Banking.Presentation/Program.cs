using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Data.Context;
using BankingMicroservices.RabbitMQ.Demo.Banking.Infra.Ioc;
using BankingMicroservices.RabbitMQ.Demo.Presentation.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services, repositories, and automapper
builder.Services.RegisterRepositories()
                .RegisterServices()
                .RegisterAutoMapper()
                .RegisterRequestValidator();
// Register DbContext
builder.Services.AddDbContext<AccountDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("AccountDbCS"))
);// ConnectionString is on User Secrets
// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        BankingMicroservices.RabbitMQ.Demo.Application.AssemblyReference.Assembly,
    BankingMicroservices.RabbitMQ.Demo.Banking.Application.AssemblyReference.Assembly
));
// Register Fluent Validation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(BankingMicroservices.RabbitMQ.Demo.Banking.Application.AssemblyReference.Assembly);
// Override default validation response
builder.Services.AddCustomValidationResponse();

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
