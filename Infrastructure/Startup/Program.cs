using System.Reflection;
using FluentValidation.AspNetCore;
using Infrastructure.Startup.Entities;
using Infrastructure.Startup.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Startup;

public static class Program
{
    private static readonly Assembly EntryAssembly;
    private static readonly ApplicationProvider Provider;

    static Program()
    {
        EntryAssembly = Assembly.GetEntryAssembly()!;
        Provider = new ApplicationProvider();
    }

    public static void Run(
        Action<ApplicationProvider, IConfiguration> appProviderDelegate,
        Action<IServiceCollection> servicesDelegate,
        Action<IServiceProvider> providerDelegate
    )
    {
        var args = Environment.GetCommandLineArgs();
        var builder = WebApplication.CreateBuilder(args);

        appProviderDelegate(Provider, builder.Configuration);

        if (string.IsNullOrEmpty(Provider.RedisConnection) || string.IsNullOrEmpty(Provider.RabbitMqConnection))
            throw new ArgumentException("Connection strings cannot be empty");

        builder.Services.AddCors();
        builder.Services.AddControllers();
        builder.Services.AddJwtBearerAuthentication(Provider.JwtBearerProvider);

        builder.Services.AddFluentValidation(configuration =>
        {
            configuration.RegisterValidatorsFromAssembly(EntryAssembly);
        });

        builder.Services.AddDefaultServices();

        builder.Services.AddContextWithRepositories(Provider.DbContextType, builder.Configuration);

        builder.Services.AddCommandHandlersFromAssembly(EntryAssembly);

        builder.Services.AddDistributedRedisCache(options => { options.Configuration = Provider.RedisConnection; });

        servicesDelegate(builder.Services);

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.ConfigureForJwt());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();

        app.UseCors(options =>
        {
            options
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials();
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        providerDelegate(app.Services);

        app.Run();
    }
}