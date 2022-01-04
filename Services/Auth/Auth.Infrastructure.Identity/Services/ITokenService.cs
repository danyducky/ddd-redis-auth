using Auth.Domain.Aggregates.UserAggregate;

namespace Auth.Infrastructure.Identity.Services;

public interface ITokenService
{
    void SetRefreshToken(User user, string refreshToken);
    string? GetRefreshToken(User user);
    void ForgetRefreshToken(User user);
}