using Cal2CapBlazor.Application;
using Cal2CapBlazor.Infrastructure;
using Cal2CapBlazor.Presentation.Components;
using Microsoft.AspNetCore.Components;

namespace Cal2CapBlazor.Presentation;
internal class Program
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCascadingAuthenticationState();
        builder.Services.AddControllers();
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddApplicationDI();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddScoped(sp =>
        {
            var navMan = sp.GetRequiredService<NavigationManager>();
            return new HttpClient
            {
                BaseAddress = new Uri(navMan.BaseUri)
            };
        });

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapControllers();
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
