using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.UserQueries;

public sealed class UsersQueryValidator : AbstractValidator<UsersQuery>
{
    public UsersQueryValidator()
    {
        RuleFor(query => query.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.");
    }
}
