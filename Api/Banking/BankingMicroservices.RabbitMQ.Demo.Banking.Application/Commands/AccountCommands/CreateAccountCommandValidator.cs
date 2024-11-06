using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(command => command.Balance)
            .NotEmpty().WithMessage("Balance is required")
            .GreaterThanOrEqualTo(3000).WithMessage("Balance must be greater than or equal to 3000")
            .NotNull().WithMessage("Balance can't be null");

        RuleFor(command => command.IsActive)
            .Must(value => value == true || value == false)  // Allow both true and false
            .WithMessage("IsActive must be provided as true or false.");


        RuleFor(command => command.UserId)
            .NotEmpty().WithMessage("UserId is required")
            .GreaterThan(0).WithMessage("UserId must be greater than 0");
    }
}
