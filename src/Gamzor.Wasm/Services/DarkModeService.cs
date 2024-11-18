using Microsoft.JSInterop;

namespace Gamzor.Wasm.Services;

public sealed class DarkModeService(IJSRuntime jSRuntime) : IAsyncDisposable
{
    private readonly IJSRuntime jSRuntime = jSRuntime;

    public bool IsDarkMode { get; private set; } = false;
    public EventHandler<bool>? OnDarkModeChanged;
    private IJSObjectReference? module;

    public async Task InitializeAsync()
    {
        module = await jSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/DarkModeToggle.js");
        var theme = await module.InvokeAsync<string>("getTheme");
        IsDarkMode = theme == "dark";
    }

    public async Task ToggleDarkMode()
    {
        IsDarkMode = !IsDarkMode;
        var theme = IsDarkMode ? "dark" : "light";
        await module!.InvokeVoidAsync("setTheme", theme);
        OnDarkModeChanged?.Invoke(this, IsDarkMode);
    }

    public async ValueTask DisposeAsync()
    {
        await module!.DisposeAsync();
    }
}