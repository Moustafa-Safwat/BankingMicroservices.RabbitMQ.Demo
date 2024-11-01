using FluentValidation;

namespace BankingMicroservices.RabbitMQ.Demo.Banking.Application.Commands.UserCommands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(100)
            .WithMessage("Full name must not exceed 100 characters.");

        RuleFor(command => command.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email is required.");

        RuleFor(command => command.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("A valid phone number is required.");
    }
}
