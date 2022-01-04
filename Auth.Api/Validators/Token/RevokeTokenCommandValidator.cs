using Auth.Domain.Commands.Token;

namespace Auth.Api.Validators.Token;

public class RevokeTokenCommandValidator : TokenCommandValidatorBase<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        ValidateRefreshToken();
    }
}