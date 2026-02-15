# Playwright with .NET Framework

A modern, scalable browser automation testing framework built with **Playwright** and **.NET 10** that follows the **Page Object Model (POM)** pattern. This framework is designed to make end-to-end testing easy, maintainable, and efficient.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Benefits](#benefits)
- [Installation](#installation)
- [Project Structure](#project-structure)
- [Configuration](#configuration)
- [Running Tests](#running-tests)
- [Creating Page Objects](#creating-page-objects)
- [Writing Tests](#writing-tests)
- [Best Practices](#best-practices)

---

## Overview

This test framework provides a robust foundation for browser automation testing using Playwright. It integrates:

- **Playwright**: Cross-platform browser automation library
- **NUnit**: Unit testing framework
- **Dependency Injection**: Microsoft Dependency Injection Container
- **Logging**: Built-in logging support
- **Configuration**: JSON-based test settings management

---

## Architecture

The framework follows a **layered architecture** pattern:

```
┌─────────────────────────────────────────────────────┐
│                 Tests Layer                         │
│              (SmokeTests, E2ETests)                 │
└────────────────┬────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────────┐
│              Pages Layer                            │
│      (HomePage, LoginPage, etc.)                    │
│           Extends BasePage                          │
└────────────────┬────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────────┐
│              Core Layer                             │
│  • BaseTest - Base test class with setup/teardown  │
│  • PlaywrightFactory - Browser initialization      │
│  • ServiceCollectionExtension - DI configuration   │
└────────────────┬────────────────────────────────────┘
                 │
┌────────────────▼────────────────────────────────────┐
│             Config Layer                            │
│  • ConfigManager - Settings management             │
│  • TestSettings - Configuration model              │
│  • appsettings.json - Configuration file           │
└─────────────────────────────────────────────────────┘
```

### Key Components

| Component | Purpose |
|-----------|---------|
| **BaseTest** | Base class for all tests with setup/teardown lifecycle |
| **PlaywrightFactory** | Manages browser instance creation and disposal |
| **BasePage** | Abstract base class for page objects with common methods |
| **ServiceCollectionExtension** | Automatic dependency injection registration |
| **ConfigManager** | Centralized configuration management |

---

## Benefits

✅ **Page Object Model (POM)**
- Separates test logic from page elements
- Easy maintenance and updates
- Improved code reusability

✅ **Dependency Injection**
- Loose coupling between components
- Easy testing and mocking
- Automatic page object registration

✅ **Cross-Browser Support**
- Run tests on Chromium, Firefox, and WebKit
- Configurable browser selection
- Headless and headed modes

✅ **Built-in Logging**
- Integrated logging for debugging
- Track test execution flow
- Useful for CI/CD integration

✅ **Easy Configuration**
- JSON-based settings management
- No hardcoded values
- Simple environment-based configuration

✅ **Modern .NET Stack**
- Built on .NET 10
- Latest C# language features
- Strong type safety

✅ **Clean Test Code**
- Fluent and readable assertions
- Async/await support
- Organized test structure

---

## Installation

### Prerequisites

- **.NET 10 SDK** ([Download](https://dotnet.microsoft.com/download))
- **Visual Studio Code** or **Visual Studio** (optional)
- **Git** (optional)

### Setup Steps

1. **Clone or extract the repository:**
   ```powershell
   git clone <repository-url>
   cd PlaywrightWithDotNetFramework
   ```

2. **Restore dependencies:**
   ```powershell
   dotnet restore
   ```

3. **Build the project:**
   ```powershell
   dotnet build
   ```

4. **Install Playwright browsers:**
   ```powershell
   dotnet execute PlaywrightNETScript.exe -- install-deps
   ```
   Or manually install for each browser:
   ```powershell
   playwright install chromium
   playwright install firefox
   playwright install webkit
   ```

5. **Verify installation:**
   ```powershell
   dotnet test
   ```

---

## Project Structure

```
PlaywrightWithDotNetFramework/
├── Pages/                              # Page Object Model classes
│   ├── BasePage.cs                     # Base page with common methods
│   ├── HomePage.cs                     # Example page object
│   └── [Add more pages here]
├── Tests/                              # Test files
│   ├── SmokeTests.cs                   # Smoke tests example
│   └── [Add more test files]
├── Core/                               # Core framework components
│   ├── BaseTest.cs                     # Base test class with setup/teardown
│   ├── PlaywrightFactory.cs            # Browser factory
│   └── ServiceCollectionExtension.cs   # DI configuration
├── Config/                             # Configuration management
│   ├── ConfigManager.cs                # Configuration loader
│   └── TestSettings.cs                 # Settings model
├── appsettings.json                    # Test settings (JSON config)
├── PlaywrightWithDotNetFramework.csproj # Project file
└── README.md                           # This file
```

---

## Configuration

### appsettings.json

All test configuration is managed through `appsettings.json`:

```json
{
  "TestSettings": {
    "Browser": "firefox",
    "Headless": false,
    "BaseUrl": "https://example.com",
    "Timeout": 30000
  }
}
```

### Configuration Options

| Setting | Type | Description | Default |
|---------|------|-------------|---------|
| **Browser** | string | Browser type: `chromium`, `firefox`, or `webkit` | firefox |
| **Headless** | boolean | Run browser in headless mode | false |
| **BaseUrl** | string | Application base URL for testing | https://example.com |
| **Timeout** | int | Default timeout in milliseconds | 30000 |

### Environment-Based Configuration

To use different settings for different environments, create environment-specific files:

- `appsettings.development.json`
- `appsettings.staging.json`
- `appsettings.production.json`

The ConfigManager will automatically load the appropriate file based on the environment.

---

## Running Tests

### Run All Tests

```powershell
dotnet test
```

### Run Specific Test File

```powershell
dotnet test --filter "ClassName=PlaywrightWithDotNetFramework.Tests.SmokeTests"
```

### Run Specific Test

```powershell
dotnet test --filter "Name=Verify_Homepage_Title"
```

### Run Tests with Logging

```powershell
dotnet test --verbosity minimal
```

### Run Tests in Headed Mode

Modify `appsettings.json`:
```json
"Headless": true
```

---

## Creating Page Objects

Page objects encapsulate UI elements and interactions for a specific page.

### Step 1: Create a Page Object Class

Create a new file in the `Pages/` directory:

```csharp
using Microsoft.Playwright;
using Microsoft.Extensions.Logging;

namespace PlaywrightWithDotNetFramework.Pages;

public class LoginPage : BasePage
{
    private const string UsernameSelector = "input[name='username']";
    private const string PasswordSelector = "input[name='password']";
    private const string LoginButtonSelector = "button[type='submit']";
    private const string ErrorMessageSelector = ".error-message";

    public LoginPage(IPage page, ILogger<LoginPage> logger)
        : base(page, logger)
    {
    }

    public async Task<bool> IsLoginFormVisibleAsync()
    {
        Logger.LogInformation("Checking if login form is visible");
        return await Page.IsVisibleAsync(UsernameSelector);
    }

    public async Task LoginAsync(string username, string password)
    {
        Logger.LogInformation("Logging in with username: {Username}", username);
        
        await FillAsync(UsernameSelector, username);
        await FillAsync(PasswordSelector, password);
        await ClickAsync(LoginButtonSelector);
        
        Logger.LogInformation("Login form submitted");
    }

    public async Task<string> GetErrorMessageAsync()
    {
        Logger.LogInformation("Retrieving error message");
        return await Page.TextContentAsync(ErrorMessageSelector) ?? "";
    }
}
```

### Step 2: Use in Tests

The page object is automatically registered via dependency injection (see `ServiceCollectionExtension.cs`). Use it in your tests:

```csharp
var loginPage = GetPage<LoginPage>();
await loginPage.LoginAsync("user@example.com", "password123");
```

---

## Writing Tests

### Basic Test Structure

```csharp
using NUnit.Framework;
using PlaywrightWithDotNetFramework.Core;
using PlaywrightWithDotNetFramework.Pages;

namespace PlaywrightWithDotNetFramework.Tests;

public class LoginTests : BaseTest
{
    [Test]
    public async Task Login_WithValidCredentials_ShowsDashboard()
    {
        // Arrange
        var loginPage = GetPage<LoginPage>();
        
        // Act
        var isFormVisible = await loginPage.IsLoginFormVisibleAsync();
        Assert.That(isFormVisible, Is.True);
        
        await loginPage.LoginAsync("user@example.com", "password");
        
        // Assert
        // Add assertions to verify successful login
    }

    [Test]
    public async Task Login_WithInvalidCredentials_ShowsError()
    {
        // Arrange
        var loginPage = GetPage<LoginPage>();
        
        // Act
        await loginPage.LoginAsync("invalid@example.com", "wrongpassword");
        
        // Assert
        var errorMessage = await loginPage.GetErrorMessageAsync();
        Assert.That(errorMessage, Does.Contain("Invalid credentials"));
    }
}
```

### Test Lifecycle

1. **[SetUp]** - Runs before each test
   - Initializes browser and page
   - Registers page objects via DI
   - Navigates to base URL

2. **Test Method** - Your test logic
   - Use `GetPage<T>()` to retrieve page objects
   - Write assertions

3. **[TearDown]** - Runs after each test
   - Closes page context
   - Closes browser
   - Disposes resources

---

## Best Practices

### 1. Use Meaningful Test Names
```csharp
// ✅ Good
[Test]
public async Task Login_WithValidCredentials_DisplaysDashboard()

// ❌ Avoid
[Test]
public async Task Test1()
```

### 2. Follow Arrange-Act-Assert (AAA) Pattern
```csharp
[Test]
public async Task Example()
{
    // Arrange - Setup test data
    var loginPage = GetPage<LoginPage>();
    
    // Act - Execute the action
    await loginPage.LoginAsync("user@example.com", "password");
    
    // Assert - Verify the result
    Assert.That(await homePage.IsLoggedInAsync(), Is.True);
}
```

### 3. Use Selectors from Page Objects
- Define selectors as constants in page classes
- Avoid hardcoding selectors in tests
- Makes maintenance easier

### 4. Handle Waits Properly
Playwright handles waiting automatically, but be explicit when needed:

```csharp
await Page.WaitForNavigationAsync();
await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
```

### 5. Use Logging for Debugging
```csharp
Logger.LogInformation("Attempting login");
Logger.LogWarning("Unexpected state detected");
Logger.LogError("Test failed with error: {Error}", ex.Message);
```

### 6. Keep Tests Independent
- Each test should be runnable in isolation
- Don't share state between tests
- Clean up resources in [TearDown]

### 7. Use Data-Driven Testing
```csharp
[TestCase("user1@example.com", "password1")]
[TestCase("user2@example.com", "password2")]
public async Task Login_MultipleUsers(string email, string password)
{
    var loginPage = GetPage<LoginPage>();
    await loginPage.LoginAsync(email, password);
    // Assert
}
```

### 8. Organize Tests by Feature
Group related tests in the same test class or separate files:

```
Tests/
├── SmokeTests.cs
├── LoginTests.cs
├── DashboardTests.cs
├── CheckoutTests.cs
```

---

## Troubleshooting

### Issue: `Playwright browsers not installed`

**Solution:**
```powershell
playwright install
# or
dotnet execute PlaywrightNETScript.exe -- install-deps
```

### Issue: `Tests timeout`

**Solution:** Increase timeout in `appsettings.json`:
```json
"Timeout": 60000
```

### Issue: `Browser fails to launch`

**Solution:** Try with a different browser in `appsettings.json`:
```json
"Browser": "chromium"
```

### Issue: `Selector not found`

**Solution:**
- Verify the selector with browser dev tools
- Update the selector in the page object
- Add explicit wait: `await Page.WaitForSelectorAsync(selector);`

---

## Resources

- [Playwright Documentation](https://playwright.dev/dotnet/)
- [NUnit Documentation](https://docs.nunit.org/)
- [Microsoft Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Page Object Model (POM)](https://www.selenium.dev/documentation/test_practices/encouraged/page_object_models/)

---

## License

MIT License - Feel free to use and modify

---

## Support

For issues, questions, or improvements, please create an issue in the repository.