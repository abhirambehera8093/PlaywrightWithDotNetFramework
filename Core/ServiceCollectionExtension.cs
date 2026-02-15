using Microsoft.Extensions.DependencyInjection;
using PlaywrightWithDotNetFramework.Pages;
using System.Reflection;

namespace PlaywrightWithDotNetFramework.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPageObjects(this IServiceCollection services)
    {
        var pageType = typeof(BasePage);

        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                t.IsSubclassOf(pageType));

        foreach (var type in types)
        {
            services.AddTransient(type);
        }

        return services;
    }
}
