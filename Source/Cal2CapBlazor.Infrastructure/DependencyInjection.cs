using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Infrastructure.Persistence;
using Cal2CapBlazor.Infrastructure.Persistence.Repositories;
using Cal2CapBlazor.Infrastructure.Services;
using Cal2CapBlazor.Application.Meals;
using Cal2CapBlazor.Application.Accounts;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Cal2CapBlazor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<CustomAuthStateProvider>();
        services.AddScoped<AuthenticationStateProvider>(provider => 
            provider.GetRequiredService<CustomAuthStateProvider>());

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

        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
        
        services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMealRepository, MealRepository>();

        services.AddScoped<ICurrentUserService, AspNetCurrentUserService>();
        services.AddSingleton<IPasswordHasherService, AspNetPasswordHasher>();

        return services;
    }
}