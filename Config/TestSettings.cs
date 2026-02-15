namespace PlaywrightWithDotNetFramework.Config;

public class TestSettings
{
    public required string Browser { get; set; }
    public bool Headless { get; set; }
    public required string BaseUrl { get; set; }
    public int Timeout { get; set; }
}
