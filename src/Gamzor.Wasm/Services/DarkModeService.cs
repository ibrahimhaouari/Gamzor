using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace Gamzor.Wasm.Services;

public sealed class DarkModeService(IJSRuntime jSRuntime, ILocalStorageService localStorageService) : IAsyncDisposable
{
    private readonly IJSRuntime jSRuntime = jSRuntime;
    private readonly ILocalStorageService localStorageService = localStorageService;

    public bool IsDarkMode { get; private set; } = false;
    public EventHandler<bool>? OnDarkModeChanged;
    private IJSObjectReference? module;

    public async Task InitializeAsync()
    {
        module = await jSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/DarkModeToggle.js");
        var theme = await localStorageService.GetItemAsStringAsync("theme");
        if (string.IsNullOrWhiteSpace(theme))
        {
            // Get the system theme
            theme = await module.InvokeAsync<string>("getSystemTheme");
        }

        IsDarkMode = theme == "dark";
        await module.InvokeVoidAsync("setTheme", theme);
    }

    public async Task ToggleDarkMode()
    {
        IsDarkMode = !IsDarkMode;
        var theme = IsDarkMode ? "dark" : "light";
        await module!.InvokeVoidAsync("setTheme", theme);
        await localStorageService.SetItemAsStringAsync("theme", theme);
        OnDarkModeChanged?.Invoke(this, IsDarkMode);
    }

    public async ValueTask DisposeAsync()
    {
        await module!.DisposeAsync();
    }
}