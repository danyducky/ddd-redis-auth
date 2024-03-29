﻿using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Infrastructure.Startup.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static void AddCommandHandlersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        services.Scan(scan =>
        {
            scan.FromAssemblies(assembly)
                .AddClasses(x => x.Where(type => IsTypeValid(type) && type.Name.EndsWith("CommandHandler")))
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
    }

    internal static void AddContextWithRepositories(this IServiceCollection services, Type context,
        IConfiguration configuration)
    {
        if (context.BaseType != typeof(DbContext))
            throw new UnsupportedContentTypeException("Context base type is not DbContext!");

        services.AddContext(context, configuration);
        services.AddRepositoriesFromAssembly(context.Assembly);
    }

    private static void AddContext(this IServiceCollection services, Type context, IConfiguration configuration)
    {
        typeof(ServiceCollectionExtensions)
            .GetMethod("AddContextInternal", BindingFlags.NonPublic | BindingFlags.Static)!
            .MakeGenericMethod(context)
            .Invoke(services, new object[] {services, configuration});
    }

    // Invoked by AddContext method
    private static void AddContextInternal<TContext>(this IServiceCollection services, IConfiguration configuration)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            var connectionString = configuration.GetConnectionString(typeof(TContext).Name);

            options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });
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

    private static bool IsTypeValid(Type type) => !type.IsAbstract && !type.IsSealed && !type.IsNested;
}