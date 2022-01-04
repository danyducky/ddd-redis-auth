namespace Auth.Infrastructure.Identity.Entities;

public class TokenPack
{
    public TokenPack(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; }
    public string RefreshToken { get; }
}