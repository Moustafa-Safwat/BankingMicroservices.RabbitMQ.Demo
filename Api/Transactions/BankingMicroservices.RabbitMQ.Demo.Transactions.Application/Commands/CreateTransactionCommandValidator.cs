using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Transactions.Application.Commands;

public sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.FromAccount)
            .NotEmpty().WithMessage("FromAccount is required")
            .GreaterThan(0).WithMessage("FromAccount must be greater than 0");

        RuleFor(x => x.ToAccount)
            .NotEmpty().WithMessage("ToAccount is required")
            .GreaterThan(0).WithMessage("ToAccount must be greater than 0");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount is required")
            .GreaterThan(0).WithMessage("Amount must be greater than 0");
    }
}
