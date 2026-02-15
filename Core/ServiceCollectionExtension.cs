using Microsoft.Extensions.DependencyInjection;
using PlaywrightWithDotNetFramework.Pages;
using System.Reflection;

namespace PlaywrightWithDotNetFramework.Core;

/// <summary>
/// ServiceCollectionExtensions: Extension methods for IServiceCollection
/// 
/// Purpose: Simplify Dependency Injection configuration by providing
/// automatic registration of page objects
/// 
/// This extension scans the assembly and registers all page classes
/// that inherit from BasePage without requiring manual registration
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// AddPageObjects: Automatically discovers and registers all page objects
    /// 
    /// How it works:
    /// 1. Get the type of BasePage (parent class)
    /// 2. Get all types in the current assembly
    /// 3. Filter for classes that inherit from BasePage
    /// 4. Register each found class as Transient (new instance each time)
    /// 5. Return the service collection for method chaining
    /// 
    /// Example: If you have HomePage, LoginPage, DashboardPage classes
    /// that inherit from BasePage, they will all be automatically registered
    /// </summary>
    public static IServiceCollection AddPageObjects(this IServiceCollection services)
    {
        // Step 1: Get the base page type (we're looking for classes inheriting from this)
        var pageType = typeof(BasePage);

        // Step 2: Get all types in the current assembly and filter
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                t.IsClass &&              // Must be a class (not interface, struct, etc.)
                !t.IsAbstract &&          // Must not be abstract (can be instantiated)
                t.IsSubclassOf(pageType)  // Must inherit from BasePage
            );

        // Step 3: Register each found page class
        // Transient = new instance created each time it's requested
        foreach (var type in types)
        {
            services.AddTransient(type);
        }

        // Step 4: Return the service collection (allows method chaining)
        // This allows multiple .Add*() calls in sequence
        return services;
    }
}
