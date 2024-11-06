using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.TransactionCommands;

public sealed class WithDrawalMoneyCommandValidator:AbstractValidator<WithDrawalMoneyCommand>
{
    public WithDrawalMoneyCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
