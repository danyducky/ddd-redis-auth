using Auth.Domain.Commands.Token;

namespace Auth.Api.Validators.Token;

public class RefreshTokenCommandValidator : TokenCommandValidatorBase<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        ValidateRefreshToken();
    }
}