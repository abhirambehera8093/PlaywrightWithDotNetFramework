using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PlaywrightWithDotNetFramework.Config;

/// <summary>
/// Loads test configuration from appsettings.json and exposes it via `Settings`.
/// </summary>
public static class ConfigManager
{
    /// <summary>
    /// Loaded `TestSettings` (null if loading failed).
    /// </summary>
    public static TestSettings? Settings { get; }

    static ConfigManager()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        Settings = config.GetSection("TestSettings").Get<TestSettings>();
    }
}
