using Cal2CapBlazor.Application;
using Cal2CapBlazor.Infrastructure;
using Cal2CapBlazor.WebUI.Components;

namespace Cal2CapBlazor.WebUI;
internal class Program 
{
    static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddApplicationDI();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        WebApplication app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
            app.UseHsts();
        }
        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.UseAuthentication();
        app.UseAuthorization();

        // Minimal API TODO

        app.Run();
    }
}