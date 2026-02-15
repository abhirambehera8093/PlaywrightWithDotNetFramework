using Microsoft.Playwright;
using PlaywrightWithDotNetFramework.Config;

namespace PlaywrightWithDotNetFramework.Core;

public class PlaywrightFactory
{
    private  IPlaywright _playwright;

    public async Task<IBrowser> LaunchBrowserAsync()
    {
        _playwright = await Playwright.CreateAsync();

        var browserType = ConfigManager.Settings.Browser.ToLower();

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = ConfigManager.Settings.Headless
        };

        return browserType switch
        {
            "chromium" => await _playwright.Chromium.LaunchAsync(launchOptions),
            "firefox"  => await _playwright.Firefox.LaunchAsync(launchOptions),
            "webkit"   => await _playwright.Webkit.LaunchAsync(launchOptions),
            _ => throw new ArgumentException($"Unsupported browser: {browserType}")
        };
    }

    public async Task DisposeAsync()
    {
        _playwright?.Dispose();
        await Task.CompletedTask;
    }
}
