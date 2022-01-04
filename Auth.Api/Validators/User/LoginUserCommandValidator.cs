using Auth.Domain.Commands.User;

namespace Auth.Api.Validators.User
{
    public class LoginUserCommandValidator : UserCommandValidatorBase<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            ValidateEmail();
            ValidatePassword();
        }
    }
}
