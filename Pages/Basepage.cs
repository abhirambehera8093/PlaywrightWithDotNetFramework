using Microsoft.Playwright;
using Microsoft.Extensions.Logging;

namespace PlaywrightWithDotNetFramework.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;
    protected readonly ILogger Logger;

    protected BasePage(IPage page, ILogger logger)
    {
        Page = page;
        Logger = logger;
    }

    protected async Task ClickAsync(string selector)
    {
        Logger.LogInformation("Clicking on element: {Selector}", selector);
        await Page.ClickAsync(selector);
    }

    protected async Task FillAsync(string selector, string value)
    {
        Logger.LogInformation("Filling element: {Selector} with value: {Value}", selector, value);
        await Page.FillAsync(selector, value);
    }
}
