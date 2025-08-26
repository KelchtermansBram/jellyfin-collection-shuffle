using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jellyfin.Plugin.CollectionShuffle.Services;

/// <summary>
/// Service registration for the collection shuffle plugin.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Registers the collection shuffle services with the DI container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddCollectionShuffleServices(this IServiceCollection services)
    {
        services.AddScoped<CollectionShuffleService>();
        services.AddHostedService<PluginHostedService>();
    }
}