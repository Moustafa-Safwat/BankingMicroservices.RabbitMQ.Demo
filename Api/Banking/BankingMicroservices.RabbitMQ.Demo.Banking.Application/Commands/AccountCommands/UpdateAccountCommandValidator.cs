using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public sealed class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(command => command.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Balance must be greater than or equal to 0.");


        RuleFor(command => command.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");

        RuleFor(command => command.RowVersion)
            .NotEmpty().WithMessage("RowVersion must not be empty.");
    }
}