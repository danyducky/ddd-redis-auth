namespace Infrastructure.Startup.Entities;

public class JwtBearerProvider
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Key { get; set; }
}