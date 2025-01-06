using Api.Data;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddServiceCollection(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        services.AddDbContext<BlogDbContext>(
            options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlServerConnection")));
        services.AddTransient<UserService>();
        services.AddTransient<NewsService>();
        services.AddTransient<NoSqlDataService>();
        services.AddCors(options =>
            options.AddPolicy("CorsPolicy", policy =>
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(configuration["ClientHost"])));

        return services;
    }
}