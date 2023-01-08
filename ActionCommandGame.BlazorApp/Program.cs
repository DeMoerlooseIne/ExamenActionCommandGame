using ActionCommandGame.BlazorApp;
using ActionCommandGame.BlazorApp.Providers;
using ActionCommandGame.BlazorApp.Settings;
using ActionCommandGame.BlazorApp.Stores;
using ActionCommandGame.Sdk;
using ActionCommandGame.Sdk.Abstractions;
using ActionCommandGame.Sdk.Extensions;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

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
builder.Services.AddScoped<IPositiveGameEventApi, PositiveGameEventApi>();
builder.Services.AddScoped<INegativeGameEventApi, NegativeGameEventApi>();

//Add Authorization
//builder.Services.AddScoped<ClaimsPrincipal>(sp => sp.GetService<IHttpContextAccessor>().HttpContext.User);

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<TokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<TokenAuthenticationStateProvider>());

//Register the Sdk api classes
if (!string.IsNullOrWhiteSpace(appSettings.ApiBaseUrl)) builder.Services.AddApi(appSettings.ApiBaseUrl);

await builder.Build().RunAsync();