namespace Infrastructure.Startup.Entities;

public class ApplicationProvider
{
    public string RedisConnection { get; set; }
    public string RabbitMqConnection { get; set; }
    public Type DbContextType { get; set; }
}