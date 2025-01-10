using Api.Data;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extensions;

/// <summary>
/// Расширение для коллекции служб приложения.
/// </summary>
public static class ApplicationServiceCollectionExtension
{
    /// <summary>
    /// Добавляет необходимые службы в коллекцию служб приложения.
    /// </summary>
    /// <param name="services">Коллекция служб для добавления.</param>
    /// <param name="configuration">Конфигурация приложения, содержащая настройки.</param>
    /// <returns>Обновленная коллекция служб.</returns>
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
        services.AddTransient<ImageService>();
        services.AddTransient<NoSqlDataService>();
        services.AddCors(options =>
            options.AddPolicy("CorsPolicy", policy =>
                policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(configuration["ClientHost"])));

        return services;
    }
}