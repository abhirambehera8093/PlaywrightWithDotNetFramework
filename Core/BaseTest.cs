using Microsoft.Playwright;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlaywrightWithDotNetFramework.Config;
using PlaywrightWithDotNetFramework.Pages;

namespace PlaywrightWithDotNetFramework.Core;

public class BaseTest
{
    protected IBrowser Browser;
    protected IBrowserContext Context;
    protected IPage Page;
    protected IServiceProvider Services;


    private PlaywrightFactory _factory;

    [SetUp]
    public async Task Setup()
    {
        _factory = new PlaywrightFactory();

        Browser = await _factory.LaunchBrowserAsync();

        Context = await Browser.NewContextAsync();

        Page = await Context.NewPageAsync();



        Page.SetDefaultTimeout(ConfigManager.Settings.Timeout);

        var serviceCollection = new ServiceCollection();


        serviceCollection.AddLogging(config =>
        {
            config.AddConsole();
            config.SetMinimumLevel(LogLevel.Information);
        });
        serviceCollection.AddSingleton(Page);
        
        // Dynamically register all page classes using extension method
        serviceCollection.AddPageObjects();

        Services = serviceCollection.BuildServiceProvider();

        await Page.GotoAsync(ConfigManager.Settings.BaseUrl);
    }

    [TearDown]
    public async Task TearDown()
    {
        try
        {
            if (Services is ServiceProvider sp)
                sp.Dispose();
            else
                (Services as IDisposable)?.Dispose();

            if (Context != null)
                await Context.CloseAsync();
            
            if (Browser != null)
                await Browser.CloseAsync();
            
            if (_factory != null)
                await _factory.DisposeAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during teardown: {ex.Message}");
        }
    }

    protected T GetPage<T>() where T : class
{
    return Services.GetRequiredService<T>();
}
}
