using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Startup.Extensions;

internal static class DefaultServicesExtensions
{
    internal static void AddDefaultServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services
            .AddSingleton<IHashService, HashService>();

        services
            .AddScoped<IRedisCache, RedisCache>()
            .AddScoped<IUserIdentity, UserIdentity>();
    }
}