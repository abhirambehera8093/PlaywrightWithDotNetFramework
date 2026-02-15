using Microsoft.Playwright;
using Microsoft.Extensions.Logging;

namespace PlaywrightWithDotNetFramework.Pages;

/// <summary>
/// HomePage: Page Object representing the home page
/// 
/// Inherits from: BasePage (provides common UI interactions and logging)
/// 
/// Responsibility: 
/// Encapsulate all interactions and elements specific to the home page
/// 
/// Example methods:
/// - GetPageTitleAsync: Retrieve the page title
/// - Add more methods for home page specific actions (search, navigation, etc.)
/// 
/// Usage in tests:
/// var homePage = GetPage<HomePage>();  // Get instance from DI container
/// var title = await homePage.GetPageTitleAsync();
/// </summary>
public class HomePage : BasePage
{
    /// <summary>
    /// HomePage Constructor: Initializes the page object
    /// 
    /// Parameters:
    /// - page: IPage instance injected via Dependency Injection
    /// - logger: ILogger<HomePage> instance for logging specific to HomePage
    /// 
    /// Flow:
    /// 1. Receive page and logger from DI container
    /// 2. Pass them to the parent BasePage constructor
    /// 3. Now available as protected fields: Page and Logger
    /// </summary>
    public HomePage(IPage page, ILogger<HomePage> logger)
        : base(page, logger)  // Call parent constructor with page and logger
    {
        // Constructor typically has no additional logic beyond calling base
        // Can add page-specific initialization here if needed
    }

    /// <summary>
    /// GetPageTitleAsync: Retrieve the current page title
    /// 
    /// Returns: The browser tab title (from <title> tag in HTML)
    /// 
    /// Process:
    /// 1. Log that we're fetching the title
    /// 2. Call Page.TitleAsync() to get the actual title
    /// 3. Log the retrieved title
    /// 4. Return the title to the caller
    /// 
    /// Usage: var title = await homePage.GetPageTitleAsync();
    /// </summary>
    public async Task<string> GetPageTitleAsync()
    {
        // Step 1: Log the action
        Logger.LogInformation("Fetching page title...");

        // Step 2: Get the page title (reads from <title> element)
        var title = await Page.TitleAsync();

        // Step 3: Log the retrieved value for debugging
        Logger.LogInformation("Page title retrieved: {Title}", title);

        // Step 4: Return the title to the test/caller
        return title;
    }
    
    // TODO: Add more methods for home page specific interactions:
    // - Click navigation buttons
    // - Fill search boxes
    // - Verify page content
    // - Navigate to other pages
}
