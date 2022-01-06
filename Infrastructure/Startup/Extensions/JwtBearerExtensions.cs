using System.Text;
using Infrastructure.Core.Extensions;
using Infrastructure.Startup.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Startup.Extensions;

internal static class JwtBearerExtensions
{
    internal static void AddJwtBearerAuthentication(this IServiceCollection services, JwtBearerProvider provider)
    {
        if (!provider.IsExists()) throw new ArgumentNullException("JwtBearerProvider", "Is null");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => options.Configure(provider));
    }

    private static void Configure(this JwtBearerOptions options, JwtBearerProvider provider)
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = provider.Issuer,
            ValidAudience = provider.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(provider.Key)),
            ClockSkew = TimeSpan.Zero
        };
    }
}