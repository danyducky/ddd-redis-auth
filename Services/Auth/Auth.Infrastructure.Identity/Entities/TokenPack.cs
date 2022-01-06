namespace Auth.Infrastructure.Identity.Entities;

public class TokenPack
{
    public TokenPack(string accessToken, RefreshTokenPayload payload)
    {
        AccessToken = accessToken;
        Payload = payload;
    }

    public string AccessToken { get; }
    public RefreshTokenPayload Payload { get; }
}