using Microsoft.Playwright;
using Microsoft.Extensions.Logging;

namespace PlaywrightWithDotNetFramework.Pages;

/// <summary>
/// BasePage: Abstract base class implementing the Page Object Model (POM) pattern
/// 
/// Purpose:
/// - Encapsulates common UI interactions (click, fill, navigate)
/// - Provides logging for debugging and test reporting
/// - Centralizes page element selectors and actions
/// - Reduces code duplication across multiple page objects
/// 
/// Usage:
/// All page classes (HomePage, LoginPage, etc.) inherit from BasePage
/// and override or add specific methods for their page's functionality
/// 
/// Benefits of POM:
/// - Easy maintenance: Update selectors in one place
/// - Code reusability: Common methods available to all pages
/// - Better readability: Tests read like natural language
/// - Reduced test brittleness: Changes to UI structure don't break tests
/// </summary>
public abstract class BasePage
{
    /// <summary>
    /// Page: Playwright IPage interface representing a browser tab
    /// Provides access to all page automation methods (click, fill, navigate, etc.)
    /// Protected so subclasses can use it
    /// Readonly: Should not be reassigned
    /// </summary>
    protected readonly IPage Page;
    
    /// <summary>
    /// Logger: Logging interface for diagnostic and debug information
    /// Protected so subclasses can log their own messages
    /// Readonly: Should not be reassigned
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// BasePage Constructor: Initializes the page and logger
    /// 
    /// Parameters:
    /// - page: Playwright page instance (injected via DI)
    /// - logger: Logger instance for this page (injected via DI)
    /// 
    /// Note: This constructor is protected because BasePage is abstract
    /// Subclasses must call this constructor via base() keyword
    /// </summary>
    protected BasePage(IPage page, ILogger logger)
    {
        Page = page;      // Store page instance
        Logger = logger;  // Store logger instance
    }

    /// <summary>
    /// ClickAsync: Click an element on the page
    /// 
    /// Parameters:
    /// - selector: CSS selector of the element to click
    ///   Examples: "#submit-button", ".login-btn", "button[type='submit']"
    /// 
    /// Process:
    /// 1. Log the action for debugging
    /// 2. Find element using selector (waits up to default timeout)
    /// 3. Click the element
    /// 4. Automatically waits for navigation/loading if needed
    /// </summary>
    protected async Task ClickAsync(string selector)
    {
        // Log the action with the selector for debugging
        Logger.LogInformation("Clicking on element: {Selector}", selector);
        
        // Perform the click action
        await Page.ClickAsync(selector);
    }

    /// <summary>
    /// FillAsync: Fill an input field with text
    /// 
    /// Parameters:
    /// - selector: CSS selector of the input field
    /// - value: Text to enter into the field
    /// 
    /// Process:
    /// 1. Log the action with selector and value for debugging
    /// 2. Clear any existing content in the field
    /// 3. Type the new value
    /// 4. It's safe to use with various input types (text, password, etc.)
    /// </summary>
    protected async Task FillAsync(string selector, string value)
    {
        // Log the action with selector and value for debugging
        Logger.LogInformation("Filling element: {Selector} with value: {Value}", selector, value);
        
        // Perform the fill action (clears existing content and enters new value)
        await Page.FillAsync(selector, value);
    }
}
