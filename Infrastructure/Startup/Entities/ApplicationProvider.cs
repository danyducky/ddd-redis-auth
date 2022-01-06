namespace Infrastructure.Startup.Entities;

public class ApplicationProvider
{
    public Type DbContextType { get; set; }
    public string RedisConnection { get; set; }
    public string RabbitMqConnection { get; set; }
    public JwtBearerProvider JwtBearerProvider { get; set; }
}