using System.Text;
using Cal2CapBlazor.Application.Accounts;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Application.Meals;
using Cal2CapBlazor.Infrastructure.Authentications;
using Cal2CapBlazor.Infrastructure.Persistence;
using Cal2CapBlazor.Infrastructure.Persistence.Repositories;
using Cal2CapBlazor.Infrastructure.Services;
using Cal2CapBlazor.Infrastructure.Services.Auths;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cal2CapBlazor.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<JwtAuthStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(provider => 
            provider.GetRequiredService<JwtAuthStateProvider>());

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
        services.AddScoped<ICurrentUserService, AspNetCurrentUserService>();
        services.AddSingleton<IPasswordHasherService, AspNetPasswordHasher>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMealRepository, MealRepository>();

        return services;
    }
}