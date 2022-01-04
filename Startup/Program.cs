using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Startup.Configurations;
using Startup.Infrastructure;

namespace Startup
{
    public static class Program
    {
        private static readonly Assembly EntryAssembly;

        static Program()
        {
            EntryAssembly = Assembly.GetEntryAssembly()!;
        }

        public static void Run(Type[] contexts,
            Action<IServiceCollection> servicesDelegate,
            Action<IServiceProvider> providerDelegate)
        {
            var builder = WebApplication.CreateBuilder(Environment.GetCommandLineArgs());

            var config = builder.Configuration.GetSection("Config");

            builder.Services.AddControllers();
            builder.Services.AddJwtBearerAuthentication(builder.Configuration);

            builder.Services.AddFluentValidation(configuration =>
            {
                configuration.RegisterValidatorsFromAssembly(EntryAssembly);
            });

            builder.Services.AddDataLayers(contexts);
            builder.Services.AddApplicationDefaults();

            builder.Services.AddDistributedRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
            });

            servicesDelegate(builder.Services);

            builder.Services.AddCors();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(SwaggerConfiguration.Configure);

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

        private static void AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => options.Configure(configuration));
        }
    }
}