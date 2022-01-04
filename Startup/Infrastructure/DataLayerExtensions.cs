using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Reflection;

namespace Startup.Infrastructure
{
    internal static class DataLayerExtensions
    {
        public static void AddDataLayers(this IServiceCollection services, Type[] contexts)
        {
            foreach (var context in contexts)
            {
                services.AddContext(context);
                services.AddRepositoriesFromAssembly(context.Assembly);
            }

            services.AddCommandHandlers();
        }

        private static void AddContext(this IServiceCollection services, Type context)
        {
            typeof(DataLayerExtensions)
                .GetMethod("AddContextInternal", BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(context)
                .Invoke(services, new object[] {services});
        }

        // Called by reflection in 'AddContext' method!
        private static void AddContextInternal<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddDbContext<TContext>();
        }

        private static void AddRepositoriesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblies(assembly)
                    .AddClasses(x => x.Where(type => IsTypeValid(type) && type.Name.EndsWith("Repository")))
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
        }

        private static void AddCommandHandlers(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
                scan.FromAssemblies(Assembly.GetEntryAssembly()!)
                    .AddClasses(x => x.Where(type => IsTypeValid(type) && type.Name.EndsWith("CommandHandler")))
                    .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
        }

        private static bool IsTypeValid(Type type) => !type.IsAbstract && !type.IsSealed && !type.IsNested;
    }
}