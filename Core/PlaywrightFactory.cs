using Microsoft.Playwright;
using PlaywrightWithDotNetFramework.Config;

namespace PlaywrightWithDotNetFramework.Core;

/// <summary>
/// PlaywrightFactory: Factory class for browser initialization and management
/// 
/// Responsibilities:
/// - Creates Playwright instance
/// - Reads browser configuration from appsettings.json
/// - Launches the appropriate browser (Chromium, Firefox, WebKit)
/// - Applies browser launch options (headless mode, etc.)
/// - Handles browser lifecycle
/// </summary>
public class PlaywrightFactory
{
    // Instance of Playwright - entry point to Playwright library
    private IPlaywright _playwright;

    /// <summary>
    /// LaunchBrowserAsync: Initializes and launches a browser instance
    /// 
    /// Process:
    /// 1. Create Playwright instance
    /// 2. Read browser type from configuration ("chromium", "firefox", "webkit")
    /// 3. Apply browser launch options (headless mode from config)
    /// 4. Launch the appropriate browser type
    /// 
    /// Returns: IBrowser instance ready for test automation
    /// </summary>
    public async Task<IBrowser> LaunchBrowserAsync()
    {
        // Step 1: Initialize Playwright (connects to browser automation protocol)
        _playwright = await Playwright.CreateAsync();

        // Step 2: Get browser type from configuration and normalize to lowercase
        // Supported values: "chromium", "firefox", "webkit"
        var browserType = ConfigManager.Settings.Browser.ToLower();

        // Step 3: Configure browser launch options
        var launchOptions = new BrowserTypeLaunchOptions
        {
            // Headless mode: true = no visible browser window, false = visible browser window
            Headless = ConfigManager.Settings.Headless
        };

        // Step 4: Launch the appropriate browser based on configuration
        // Uses switch expression for clean, readable browser selection
        // Throws ArgumentException if browser type is not supported
        return browserType switch
        {
            "chromium" => await _playwright.Chromium.LaunchAsync(launchOptions),  // Google Chrome
            "firefox"  => await _playwright.Firefox.LaunchAsync(launchOptions),    // Mozilla Firefox
            "webkit"   => await _playwright.Webkit.LaunchAsync(launchOptions),     // Safari
            _ => throw new ArgumentException($"Unsupported browser: {browserType}")  // Error for invalid browser
        };
    }

    /// <summary>
    /// DisposeAsync: Cleans up Playwright resources
    /// Ensures proper cleanup of browser automation resources
    /// </summary>
    public async Task DisposeAsync()
    {
        // Dispose Playwright instance (if not null)
        // This releases all internal resources
        _playwright?.Dispose();
        
        // Return completed task (this method is async for consistency)
        await Task.CompletedTask;
    }
}
