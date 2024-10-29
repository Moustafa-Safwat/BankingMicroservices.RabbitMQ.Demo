using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BankingMicroservices.RabbitMQ.Demo.Presentation.Configuration;

public static class ValidationCustomResponse
{
    public static IServiceCollection AddCustomValidationResponse(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .Select(x => new Error(x.Key, string.Join(" , ", x.Value?.Errors.Select(er => er.ErrorMessage) ?? [])))
                .ToList();

                var responseObj = Response<object>.Create(Result.Failures(errors), StatusCode.BadRequest);
                return new BadRequestObjectResult(responseObj);
            };
        });
        return services;
    }
}
