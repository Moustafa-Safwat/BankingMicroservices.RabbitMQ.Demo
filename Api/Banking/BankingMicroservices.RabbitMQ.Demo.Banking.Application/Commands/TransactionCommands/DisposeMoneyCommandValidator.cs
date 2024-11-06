using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.TransactionCommands;

public class DisposeMoneyCommandValidator:AbstractValidator<DisposeMoneyCommand>
{
    public DisposeMoneyCommandValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("AccountId is required.");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required.")
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
    }
}
