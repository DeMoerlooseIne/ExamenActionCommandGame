using ActionCommandGame.BlazorApp;
using ActionCommandGame.BlazorApp.Providers;
using ActionCommandGame.BlazorApp.Settings;
using ActionCommandGame.BlazorApp.Stores;
using ActionCommandGame.Sdk.Abstractions;
using ActionCommandGame.Sdk.Extensions;
using Blazored.LocalStorage;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSyncfusionBlazor();

var appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ITokenStore, TokenStore>();

//Add Authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => 
   provider.GetRequiredService<TokenAuthenticationStateProvider>());

//Services.AddScoped(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

//Register the Sdk api classes
if (!string.IsNullOrWhiteSpace(appSettings.ApiBaseUrl))
{
    builder.Services.AddApi(appSettings.ApiBaseUrl);
}

await builder.Build().RunAsync();
