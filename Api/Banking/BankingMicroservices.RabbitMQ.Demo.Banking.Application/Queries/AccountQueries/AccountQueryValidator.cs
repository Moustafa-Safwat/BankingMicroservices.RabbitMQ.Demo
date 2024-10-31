using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.AccountQueries;

public sealed class AccountQueryValidator:AbstractValidator<AccountQuery>
{
    public AccountQueryValidator()
    {
        RuleFor(query=>query.Id)
            .GreaterThan(0)
            .WithMessage("Account id must be greater than 0.");
    }
}
