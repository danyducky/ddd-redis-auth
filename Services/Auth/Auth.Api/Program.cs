using Auth.Infrastructure;
using Auth.Infrastructure.Identity.Services;

Infrastructure.Startup.Program.Run(
(provider) =>
{
    provider.DbContextType = typeof(AuthContext);
    provider.RedisConnection = "localhost:6379";
    provider.RabbitMqConnection = "localhost";
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