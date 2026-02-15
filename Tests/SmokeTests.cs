using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using PlaywrightWithDotNetFramework.Core;
using PlaywrightWithDotNetFramework.Pages;

namespace PlaywrightWithDotNetFramework.Tests;

public class SmokeTests : BaseTest
{
    [Test]
public async Task Verify_Homepage_Title()
{
    var homePage = GetPage<HomePage>();

    var title = await homePage.GetPageTitleAsync();

    Assert.That(title, Is.Not.Null);
}

}
