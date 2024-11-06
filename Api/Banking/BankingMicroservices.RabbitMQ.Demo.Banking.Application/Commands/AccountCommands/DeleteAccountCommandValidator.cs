using FluentValidation;
using FluentValidation.Validators;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.AccountCommands;

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is required")
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}
