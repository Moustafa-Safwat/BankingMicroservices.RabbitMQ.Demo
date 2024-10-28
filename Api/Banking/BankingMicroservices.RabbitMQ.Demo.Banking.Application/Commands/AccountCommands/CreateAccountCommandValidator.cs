using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(account => account.Balance)
            .NotEmpty().WithMessage("Balance is required")
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to 0");

        RuleFor(account => account.IsActive)
            .NotEmpty().WithMessage("IsActive is requidred");

        RuleFor(account => account.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .GreaterThanOrEqualTo(0).WithMessage("UserId must be greater than or equal to 0");
    }
}
