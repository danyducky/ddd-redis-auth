using Auth.Domain.Commands.User;
using FluentValidation;

namespace Auth.Api.Validators.User
{
    public abstract class UserCommandValidatorBase<TCommand> : AbstractValidator<TCommand> where TCommand : UserCommandBase
    {
        protected void ValidateEmail()
        {
            RuleFor(command => command.Email)
                .EmailAddress()
                .WithSeverity(Severity.Error)
                .WithMessage("Invalid email.");
        }

        protected void ValidatePassword()
        {
            RuleFor(command => command.Password)
                .MinimumLength(6)
                .WithSeverity(Severity.Error)
                .WithMessage("Invalid password.");
        }
    }
}
