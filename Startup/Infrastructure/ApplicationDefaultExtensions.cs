using Common.Services;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Startup.Infrastructure
{
    internal static class ApplicationDefaultExtensions
    {
        public static void AddApplicationDefaults(this IServiceCollection services)
        {
            services.AddEntryAssemblyDefaults();

            services.AddScoped<IRedisCache, RedisCache>();

            services.AddSingleton<IHashService, HashService>();
        }

        private static void AddEntryAssemblyDefaults(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblies(Assembly.GetEntryAssembly()!)
                    .AddClasses(x => x.Where(type => IsTypeValid(type)
                                                     && (
                                                         type.Name.EndsWith("Handler") ||
                                                         type.Name.EndsWith("ModelBuilder")
                                                     )
                    ))
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsMatchingInterface()
                    .WithScopedLifetime();
            });
        }

        private static bool IsTypeValid(Type type) => !type.IsAbstract && !type.IsSealed && !type.IsNested;
    }
}