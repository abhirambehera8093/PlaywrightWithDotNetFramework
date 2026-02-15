namespace PlaywrightWithDotNetFramework.Config;

/// <summary>
/// TestSettings: Data model for test configuration properties
/// 
/// This class holds all test settings that are loaded from appsettings.json
/// 
/// Configuration mapping:
/// JSON properties are automatically mapped to C# properties via ConfigManager
/// 
/// Example appsettings.json:
/// {
///   "TestSettings": {
///     "Browser": "chromium",
///     "Headless": true,
///     "BaseUrl": "https://example.com",
///     "Timeout": 30000
///   }
/// }
/// </summary>
public class TestSettings
{
    /// <summary>
    /// Browser: Type of browser to use for testing
    /// Supported values: "chromium", "firefox", "webkit"
    /// Required: Yes (must be specified in appsettings.json)
    /// </summary>
    public required string Browser { get; set; }
    
    /// <summary>
    /// Headless: Whether to run browser in headless mode
    /// true = no visible browser window (faster, better for CI/CD)
    /// false = visible browser window (useful for debugging)
    /// Default: false (shows browser window)
    /// </summary>
    public bool Headless { get; set; }
    
    /// <summary>
    /// BaseUrl: Starting URL for tests
    /// Example: "https://example.com"
    /// Used by BaseTest to navigate to initial page
    /// Required: Yes (must be specified in appsettings.json)
    /// </summary>
    public required string BaseUrl { get; set; }
    
    /// <summary>
    /// Timeout: Default timeout for page operations in milliseconds
    /// Example: 30000 = 30 seconds
    /// Applies to: page navigation, element waits, etc.
    /// Default: varies (typically 30000)
    /// </summary>
    public int Timeout { get; set; }
}
