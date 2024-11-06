using BankingMicroservices.RabbitMQ.Demo.Infra.Ioc;
using BankingMicroservices.RabbitMQ.Demo.Presentation.Configuration;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Data.Context;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Infra.Ioc;
using BankingMicroservices.RabbitMQ.Demo.Transactions.Presentation.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var version = builder.Configuration.GetSection("Swagger:Version").Value;
var title = builder.Configuration.GetSection("Swagger:Title").Value;
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc(version, new()
    {
        Title = title,
        Version = version,
        Description = builder.Configuration.GetSection("Swagger:Description").Value
    });
});

// Register DbContext
builder.Services.AddDbContext<TransactionDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TransactionsDbCS"));
});

// Register Services, Repositories, and Automapper
builder.Services.RegisterRepositories()
                .RegisterServices()
                .RegisterAutoMapper()
                .RegisterEvents()
                .RegisterEventBus();

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        BankingMicroservices.RabbitMQ.Demo.Application.AssemblyReference.Assembly,
    BankingMicroservices.RabbitMQ.Demo.Transactions.Application.AssemblyReference.Assembly
));
// Register Fluent Validation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(BankingMicroservices.RabbitMQ.Demo.Transactions.Application.AssemblyReference.Assembly);
// Override default validation response
builder.Services.AddCustomValidationResponse();

var app = builder.Build();

app.ConfigureEventBus();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", title);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
