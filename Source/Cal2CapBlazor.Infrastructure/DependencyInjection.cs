using Cal2CapBlazor.Application.Accounts;
using Cal2CapBlazor.Application.Common.Interfaces;
using Cal2CapBlazor.Application.Meals;
using Cal2CapBlazor.Infrastructure.Authentications;
using Cal2CapBlazor.Infrastructure.Persistence;
using Cal2CapBlazor.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cal2CapBlazor.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "Auth-Token";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;

                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });

        services.AddScoped<IApplicationDbContext>(provider => 
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUserContext, ServerUserContext>();
        services.AddSingleton<IPasswordHasherService, AspNetPasswordHasher>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IMealRepository, MealRepository>();

        return services;
    }
}