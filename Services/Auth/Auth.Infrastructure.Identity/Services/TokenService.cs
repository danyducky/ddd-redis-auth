using Auth.Infrastructure.Identity.Entities;
using Infrastructure.Services;

namespace Auth.Infrastructure.Identity.Services;

public class TokenService : ITokenService
{
    private readonly IRedisCache _redisCache;

    private const int RefreshTokenExpirationDays = 1;

    public TokenService(IRedisCache redisCache)
    {
        _redisCache = redisCache;
    }

    public void Set(RefreshTokenPayload payload)
    {
        _redisCache.Set(payload.RefreshToken, payload, (options) =>
        {
            options.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(RefreshTokenExpirationDays);
        });
    }

    public RefreshTokenPayload? Get(string refreshToken)
    {
        return _redisCache.Get<RefreshTokenPayload>(refreshToken);
    }

    public void Forget(string refreshToken)
    {
        _redisCache.Clear(refreshToken);
    }
}