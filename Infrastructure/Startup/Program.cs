using System.Reflection;
using FluentValidation.AspNetCore;
using Infrastructure.Startup.Entities;
using Infrastructure.Startup.Extensions;
using Microsoft.AspNetCore.Builder;
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
        Action<ApplicationProvider> appProviderDelegate,
        Action<IServiceCollection> servicesDelegate,
        Action<IServiceProvider> providerDelegate
    )
    {
        var args = Environment.GetCommandLineArgs();
        var builder = WebApplication.CreateBuilder(args);

        appProviderDelegate(Provider);

        if (string.IsNullOrEmpty(Provider.RedisConnection) || string.IsNullOrEmpty(Provider.RabbitMqConnection))
            throw new ArgumentException("Connection strings cannot be empty");

        builder.Services.AddControllers();
        builder.Services.AddJwtBearerAuthentication(builder.Configuration);

        builder.Services.AddFluentValidation(configuration =>
        {
            configuration.RegisterValidatorsFromAssembly(EntryAssembly);
        });

        builder.Services.AddDefaultServices();
        builder.Services.AddContextWithRepositories(Provider.DbContextType);
        builder.Services.AddCommandHandlersFromAssembly(EntryAssembly);

        builder.Services.AddDistributedRedisCache(options => { options.Configuration = Provider.RedisConnection; });

        servicesDelegate(builder.Services);

        builder.Services.AddCors();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.ConfigureForJwt());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        providerDelegate(app.Services);

        app.Run();
    }
}