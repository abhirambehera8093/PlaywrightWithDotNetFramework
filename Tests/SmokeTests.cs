using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using PlaywrightWithDotNetFramework.Core;
using PlaywrightWithDotNetFramework.Pages;

namespace PlaywrightWithDotNetFramework.Tests;

/// <summary>
/// SmokeTests: Collection of smoke tests for basic functionality validation
/// 
/// Smoke Testing:
/// - Quick, high-level tests to verify basic functionality works
/// - Run before more detailed tests to catch major issues early
/// - Should be fast and cover critical user flows
/// 
/// Inherits from: BaseTest
/// Provides: Browser setup/teardown, page object access via DI
/// 
/// Test Flow for each [Test] method:
/// 1. NUnit detects [Test] attribute
/// 2. BaseTest.Setup() called - browser initialized
/// 3. Test method executes
/// 4. NUnit asserts pass/fail
/// 5. BaseTest.TearDown() called - browser closed
/// 
/// Usage: Run with 'dotnet test' or NUnit test runner
/// </summary>
public class SmokeTests : BaseTest
{
    /// <summary>
    /// Verify_Homepage_Title: Test that the home page loads successfully
    /// 
    /// Test Case:
    /// 1. Navigate to base URL (done in BaseTest.Setup)
    /// 2. Get the page title
    /// 3. Assert that title is not null (page loaded successfully)
    /// 
    /// Purpose:
    /// - Verify basic page load functionality
    /// - Ensure browser can navigate to the application
    /// - Confirm page responds with valid HTML
    /// 
    /// Expected Result: Test passes if title is retrieved successfully
    /// 
    /// Naming Convention: Verify_Action_ExpectedResult
    /// </summary>
    [Test]
    public async Task Verify_Homepage_Title()
    {
        // Arrange: Get the HomePage object from Dependency Injection
        // HomePage is automatically registered in BaseTest.Setup()
        var homePage = GetPage<HomePage>();

        // Act: Get the page title (browser tab title)
        var title = await homePage.GetPageTitleAsync();

        // Assert: Verify that title was retrieved (not null)
        // If page didn't load, title would be null
        Assert.That(title, Is.Not.Null);
    }
    
    // TODO: Add more smoke tests:
    // [Test]
    // public async Task Verify_Navigation_To_LoginPage() { ... }
    // 
    // [Test]
    // public async Task Verify_Page_Response_Time() { ... }
    // 
    // [Test]
    // public async Task Verify_Basic_Page_Layout() { ... }
}
