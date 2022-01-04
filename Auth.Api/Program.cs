using Auth.Infrastructure;
using Auth.Infrastructure.Identity.Services;

Startup.Program.Run(new[]
{
    typeof(AuthContext)
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