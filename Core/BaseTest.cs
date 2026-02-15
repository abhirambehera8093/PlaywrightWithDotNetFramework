using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlaywrightWithDotNetFramework.Config;
using PlaywrightWithDotNetFramework.Pages;

namespace PlaywrightWithDotNetFramework.Core;

/// <summary>
/// BaseTest: Base class for all NUnit test classes
/// 
/// This class provides the foundation for test execution:
/// - Initializes browser and page instances before each test ([SetUp])
/// - Cleans up resources after each test ([TearDown])
/// - Provides dependency injection container for page objects
/// - Manages test lifecycle and configuration
/// 
/// Flow:
/// 1. NUnit calls [SetUp] before each test method
/// 2. Browser instance is created using PlaywrightFactory
/// 3. Page object is registered in DI container
/// 4. Test method executes and uses GetPage<T>() to retrieve page objects
/// 5. NUnit calls [TearDown] after each test
/// 6. All resources are disposed and browser is closed
/// </summary>
public class BaseTest
{
    // Browser instance - controls browser process
    protected IBrowser Browser;
    
    // Browser context - isolated browser session (cookies, local storage, etc.)
    protected IBrowserContext Context;
    
    // Page instance - represents a single tab/page in the browser
    protected IPage Page;
    
    // Dependency Injection container - holds all registered services (page objects, logging, etc.)
    protected IServiceProvider Services;

    // Factory for creating and disposing browser instances
    private PlaywrightFactory _factory;

    /// <summary>
    /// Setup method: Runs BEFORE each test
    /// Initializes browser, page, and dependency injection container
    /// </summary>
    [SetUp]
    public async Task Setup()
    {
        // Step 1: Create Playwright factory for browser management
        _factory = new PlaywrightFactory();

        // Step 2: Launch the browser (Chromium, Firefox, or WebKit based on appsettings.json)
        Browser = await _factory.LaunchBrowserAsync();

        // Step 3: Create an isolated browser context (like an incognito window)
        Context = await Browser.NewContextAsync();

        // Step 4: Create a new page (tab) within the context
        Page = await Context.NewPageAsync();

        // Step 5: Set default timeout for all page operations (in milliseconds)
        // This applies to navigation, element waits, etc.
        Page.SetDefaultTimeout(ConfigManager.Settings.Timeout);

        // Step 6: Setup Dependency Injection Container
        var serviceCollection = new ServiceCollection();

        // Step 7: Configure logging (outputs to console with Information level and above)
        serviceCollection.AddLogging(config =>
        {
            config.AddConsole();  // Log to console
            config.SetMinimumLevel(LogLevel.Information);  // Show Information, Warning, Error levels
        });
        
        // Step 8: Register the Page instance in DI container (so page objects can use it)
        serviceCollection.AddSingleton(Page);
        
        // Step 9: Automatically register all page classes (HomePage, LoginPage, etc.)
        // This extension method scans for classes extending BasePage and registers them
        serviceCollection.AddPageObjects();

        // Step 10: Build the service provider (finalizes DI container)
        Services = serviceCollection.BuildServiceProvider();

        // Step 11: Navigate to the base URL from appsettings.json
        // This is the starting point for the test
        await Page.GotoAsync(ConfigManager.Settings.BaseUrl);
    }

    /// <summary>
    /// TearDown method: Runs AFTER each test
    /// Cleans up browser and resources to ensure no memory leaks
    /// </summary>
    [TearDown]
    public async Task TearDown()
    {
        try
        {
            // Step 1: Dispose the service provider (releases all registered services)
            if (Services is ServiceProvider sp)
                sp.Dispose();
            else
                (Services as IDisposable)?.Dispose();

            // Step 2: Close the browser context (clears cookies, cache, etc.)
            if (Context != null)
                await Context.CloseAsync();
            
            // Step 3: Close the browser instance (stops the browser process)
            if (Browser != null)
                await Browser.CloseAsync();
            
            // Step 4: Dispose the Playwright factory
            if (_factory != null)
                await _factory.DisposeAsync();
        }
        catch (Exception ex)
        {
            // If an error occurs during cleanup, log it but don't fail the test
            Console.WriteLine($"Error during teardown: {ex.Message}");
        }
    }

    /// <summary>
    /// GetPage: Generic method to retrieve a page object from the DI container
    /// 
    /// Usage: var homePage = GetPage<HomePage>();
    /// This allows tests to access page objects that were automatically registered
    /// 
    /// Type constraints:
    /// - T must be a reference type (class)
    /// </summary>
    protected T GetPage<T>() where T : class
    {
        // Request the page object from the DI container
        // If it's not registered, throws InvalidOperationException
        return Services.GetRequiredService<T>();
}
}
