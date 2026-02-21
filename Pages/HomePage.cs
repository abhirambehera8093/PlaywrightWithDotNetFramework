using Microsoft.Playwright;
using Microsoft.Extensions.Logging;

namespace PlaywrightWithDotNetFramework.Pages;

public class HomePage : BasePage
{
    public HomePage(IPage page, ILogger<HomePage> logger) : base(page, logger)
    {
    }

    public async Task<string> GetPageTitleAsync()
    {
        Logger.LogInformation("Fetching page title...");

        var title = await Page.TitleAsync();

        Logger.LogInformation("Page title retrieved: {Title}", title);

        return title;
    }
}
