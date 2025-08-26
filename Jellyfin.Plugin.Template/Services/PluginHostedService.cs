using System;
using System.Threading;
using System.Threading.Tasks;
using Jellyfin.Plugin.CollectionShuffle.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.CollectionShuffle.Services;

/// <summary>
/// Hosted service for the collection shuffle plugin.
/// </summary>
public class PluginHostedService : IHostedService
{
    private readonly ILogger<PluginHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly PluginConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="PluginHostedService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="configuration">The plugin configuration.</param>
    public PluginHostedService(
        ILogger<PluginHostedService> logger,
        IServiceProvider serviceProvider,
        PluginConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Collection Shuffle Plugin starting...");

            // Log current configuration
            _logger.LogInformation("Plugin Configuration:");
            _logger.LogInformation("  Enable Collection Shuffle: {EnableShuffle}", _configuration.EnableCollectionShuffle);
            _logger.LogInformation("  Prevent Back-to-Back Shows: {PreventBackToBack}", _configuration.PreventBackToBackShows);
            _logger.LogInformation("  Shuffle Shows First: {ShuffleShowsFirst}", _configuration.ShuffleShowsFirst);
            _logger.LogInformation("  Shuffle Episodes Second: {ShuffleEpisodesSecond}", _configuration.ShuffleEpisodesSecond);
            _logger.LogInformation("  Max Consecutive Shows: {MaxConsecutive}", _configuration.MaxConsecutiveShowsFromSameSeries);

            // Initialize services
            using var scope = _serviceProvider.CreateScope();
            var shuffleService = scope.ServiceProvider.GetRequiredService<CollectionShuffleService>();

            _logger.LogInformation("Collection Shuffle Plugin started successfully");

            await Task.CompletedTask.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting Collection Shuffle Plugin");
        }
    }

    /// <inheritdoc />
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Collection Shuffle Plugin stopping...");

            // Cleanup code if needed

            _logger.LogInformation("Collection Shuffle Plugin stopped successfully");

            await Task.CompletedTask.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error stopping Collection Shuffle Plugin");
        }
    }
}