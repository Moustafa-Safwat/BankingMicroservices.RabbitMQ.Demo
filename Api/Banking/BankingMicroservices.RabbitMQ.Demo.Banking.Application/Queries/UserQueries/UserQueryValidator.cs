using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Queries.UserQueries;

public class UserQueryValidator : AbstractValidator<UserQuery>
{
    public UserQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");
    }
}
