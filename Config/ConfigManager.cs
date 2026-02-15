using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PlaywrightWithDotNetFramework.Config;

public static class ConfigManager
{
    public static TestSettings? Settings { get; }

    static ConfigManager()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        Settings = config!
            .GetSection("TestSettings")
            .Get<TestSettings>();
        
    }
}
