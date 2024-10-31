using BankingMicroservices.RabbitMQ.Demo.Application.Interfaces;
using BankingMicroservices.RabbitMQ.Demo.Application.Messaging;
using BankingMicroservices.RabbitMQ.Demo.Core.Shared;
using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Application.Services;

/// <summary>
/// Validates the result of a query request.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TQuery">The type of the query.</typeparam>
public class ResultValidator<TRequest, TQuery>(
    IValidator<TRequest> validator
    )
    : IResultValidator<TRequest, TQuery>
    where TRequest : IQuery<TQuery>
{
    /// <summary>
    /// Validates the specified query asynchronously.
    /// </summary>
    /// <param name="query">The query to validate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a read-only list of errors.</returns>
    public async Task<IReadOnlyList<Error>> QueryValidateAsync(TRequest query, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            return validationResult.Errors.Select(e => new Error(e.PropertyName, e.ErrorMessage)).ToList();
        }
        return [];
    }
}
