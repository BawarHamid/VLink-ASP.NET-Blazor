global using Microsoft.AspNetCore.Components.Authorization;
global using Microsoft.AspNetCore.Authorization;
global using Vikarlink.WebApp.Services.VikarServices;
global using Vikarlink.WebApp.Services.VagtServices;
global using Vikarlink.WebApp.Services.KlassevaerlseService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Vikarlink.WebApp;
using MudBlazor.Services;
using Vikarlink.WebApp.StateProvider;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7116") });
builder.Services.AddScoped<IVikarService, VikarService>();
builder.Services.AddScoped<IVagtService, VagtService>();
builder.Services.AddScoped<IKlassevaerelseService, KlassevaerelseService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddMudServices();
await builder.Build().RunAsync();
