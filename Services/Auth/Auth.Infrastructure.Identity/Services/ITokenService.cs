using Auth.Infrastructure.Identity.Entities;

namespace Auth.Infrastructure.Identity.Services;

public interface ITokenService
{
    void Set(RefreshTokenPayload payload);
    RefreshTokenPayload? Get(string refreshToken);
    void Forget(string refreshToken);
}