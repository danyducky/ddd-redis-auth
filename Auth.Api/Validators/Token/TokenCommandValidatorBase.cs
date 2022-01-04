using Auth.Domain.Commands.Token;
using FluentValidation;

namespace Auth.Api.Validators.Token;

public class TokenCommandValidatorBase<TCommand> : AbstractValidator<TCommand> where TCommand : TokenCommandBase
{
    protected void ValidateRefreshToken()
    {
        RuleFor(command => command.RefreshToken)
            .NotEmpty()
            .WithSeverity(Severity.Error)
            .WithMessage("Refresh token not found.");
    }
}