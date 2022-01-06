namespace Auth.Infrastructure.Identity.Entities;

public class RefreshTokenPayload
{
    public RefreshTokenPayload(string refreshToken, Guid userId)
    {
        RefreshToken = refreshToken;
        UserId = userId;
    }

    public string RefreshToken { get; }
    public Guid UserId { get; }

    public static RefreshTokenPayload Instance(string refreshToken, Guid userId) => new(refreshToken, userId);
}