using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.Authorization;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Infrastructure.Persistance;
using Cal2CapBlazor.Infrastructure.Persistance.Repositories;
using Cal2CapBlazor.Infrastructure.Services;
using Cal2CapBlazor.Application.Meals;
using Cal2CapBlazor.Application.Accounts;

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

        services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMealRepository, MealRepository>();

        services.AddScoped<ICurrentUserService, AspNetCurrentUserService>();
        services.AddSingleton<IPasswordHasherService, AspNetPasswordHasher>();

        return services;
    }
}