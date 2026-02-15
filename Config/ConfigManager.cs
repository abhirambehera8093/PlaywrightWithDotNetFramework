using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PlaywrightWithDotNetFramework.Config;

/// <summary>
/// ConfigManager: Static class for centralized configuration management
/// 
/// Purpose: Load and provide access to test settings from appsettings.json
/// 
/// This is a static class with a static constructor, ensuring:
/// - Configuration is loaded once when first accessed
/// - All tests use the same configuration throughout execution
/// - Easy access to settings from anywhere: ConfigManager.Settings.BaseUrl
/// 
/// Configuration flow:
/// 1. When ConfigManager is first used, the static constructor runs
/// 2. Reads appsettings.json from the application's base directory
/// 3. Binds JSON section "TestSettings" to TestSettings class
/// 4. Settings are cached in the Settings property
/// </summary>
public static class ConfigManager
{
    /// <summary>
    /// Settings: Public property providing access to test configuration
    /// Read-only: Prevents accidental modification of configuration during tests
    /// Nullable: May be null if configuration loading fails
    /// </summary>
    public static TestSettings? Settings { get; }

    /// <summary>
    /// Static constructor: Runs once when the class is first accessed
    /// Initializes the Settings property with values from appsettings.json
    /// </summary>
    static ConfigManager()
    {
        // Step 1: Create a configuration builder
        var config = new ConfigurationBuilder()
            // Step 2: Set the base path to the application's runtime directory
            // This is where appsettings.json is copied during build
            .SetBasePath(AppContext.BaseDirectory)
            
            // Step 3: Add the JSON configuration file
            // "optional: false" means the file MUST exist (error if missing)
            .AddJsonFile("appsettings.json", optional: false)
            
            // Step 4: Build the configuration (finalize and return IConfiguration)
            .Build();

        // Step 5: Extract the "TestSettings" section from the JSON
        // Step 6: Bind the JSON data to the TestSettings class properties
        // This maps JSON structure to C# class automatically
        Settings = config!
            .GetSection("TestSettings")
            .Get<TestSettings>();
    }
}
