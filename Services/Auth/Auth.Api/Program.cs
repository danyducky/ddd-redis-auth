using Auth.Infrastructure;
using Auth.Infrastructure.Identity.Services;
using Infrastructure.Startup.Entities;

Infrastructure.Startup.Program.Run(
(provider, configuration) =>
{
    provider.DbContextType = typeof(AuthContext);
    provider.RedisConnection = configuration.GetConnectionString("RedisConnection");
    provider.RabbitMqConnection = "localhost";
    
    provider.JwtBearerProvider = new JwtBearerProvider
    {
        Audience = configuration["Jwt:Audience"],
        Issuer = configuration["Jwt:Issuer"],
        Key = configuration["Jwt:Key"]
    };
},
(services) =>
{
    services.AddSingleton<IJwtFactory, JwtFactory>();

    services
        .AddScoped<ITokenService, TokenService>()
        .AddScoped<IUserManager, UserManager>();
},
(provider) =>
{

});