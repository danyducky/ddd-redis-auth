using Auth.Domain.Aggregates.UserAggregate;
using Infrastructure.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Auth.Infrastructure.Identity.Services;

public class TokenService : ITokenService
{
    private readonly IRedisCache _redisCache;

    private const int RefreshTokenExpirationDays = 1;

    public TokenService(IRedisCache redisCache)
    {
        _redisCache = redisCache;
    }

    public void SetRefreshToken(User user, string refreshToken)
    {
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(RefreshTokenExpirationDays)
        };

        _redisCache.Set(user.Id.ToString(), refreshToken, cacheOptions);
    }

    public string? GetRefreshToken(User user)
    {
        return _redisCache.Get<string?>(user.Id.ToString());
    }

    public void ForgetRefreshToken(User user)
    {
        _redisCache.Clear(user.Id.ToString());
    }
}