using Auth.Domain.Commands.User;
using FluentValidation;

namespace Auth.Api.Validators.User
{
    public class RegisterUserCommandValidator : UserCommandValidatorBase<RegisterUserCommand>
    {
        private static int MINIMUM_USER_YEARS_OLD = 14;

        public RegisterUserCommandValidator()
        {
            ValidateEmail();
            ValidatePassword();
            ValidateFirstname();
            ValidateSurname();
            ValidatePatronymic();
            ValidateBirthDate();
        }

        private void ValidateFirstname()
        {
            RuleFor(command => command.Firstname)
                .NotEmpty()
                .WithSeverity(Severity.Error)
                .WithMessage("Firstname is required.");
        }

        private void ValidateSurname()
        {
            RuleFor(command => command.Surname)
                .NotEmpty()
                .WithSeverity(Severity.Error)
                .WithMessage("Surname is required.");
        }

        private void ValidatePatronymic()
        {
            RuleFor(command => command.Patronymic)
                .NotEmpty()
                .WithSeverity(Severity.Error)
                .WithMessage("Patronymic is required.");
        }

        private void ValidateBirthDate()
        {
            RuleFor(command => command.BirthDate)
                .Must(date => date.Year <= DateTime.UtcNow.Year - MINIMUM_USER_YEARS_OLD)
                .WithSeverity(Severity.Error)
                .WithMessage("Valid birth date only for over 14 years old users.");
        }
    }
}
