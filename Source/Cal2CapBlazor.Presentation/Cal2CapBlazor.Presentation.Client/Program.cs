using Cal2CapBlazor.Presentation.Client.Infrastructure;
using Cal2CapBlazor.Presentation.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Cal2CapBlazor.Presentation.Client;
internal class Program
{
    static async Task Main(string[] args)
    {
        WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddAuthorizationCore();

        builder.Services.AddCascadingAuthenticationState();

        builder.Services.AddTransient<CookieHandler>();
        builder.Services.AddScoped<AuthenticationStateProvider, WasmAuthenticationStateProvider>();
        builder.Services.AddScoped(sp => 
        {
            HttpClientHandler handler = new HttpClientHandler();
            
            HttpClient client = new HttpClient(handler)
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };

            return client;
        });

        await builder.Build().RunAsync();
    }
}
