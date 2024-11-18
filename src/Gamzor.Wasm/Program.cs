using Blazored.LocalStorage;
using Gamzor.Wasm.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<DarkModeService>();

var host = builder.Build();
// Initialize the DarkModeService
var darkModeService = host.Services.GetRequiredService<DarkModeService>();
await darkModeService.InitializeAsync();
// Run the app
await host.RunAsync();
