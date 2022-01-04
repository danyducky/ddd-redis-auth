using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Startup.Extensions;

internal static class DefaultServicesExtensions
{
    internal static void AddDefaultServices(this IServiceCollection services)
    {
        services.AddSingleton<IHashService, HashService>();

        services.AddScoped<IRedisCache, RedisCache>();
    }
}