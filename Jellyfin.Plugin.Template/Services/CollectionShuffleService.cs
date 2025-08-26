using System;
using System.Collections.Generic;
using System.Linq;
using Jellyfin.Plugin.CollectionShuffle.Configuration;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.CollectionShuffle.Services;

/// <summary>
/// Service for handling collection shuffle functionality.
/// </summary>
public class CollectionShuffleService
{
    private readonly ILogger<CollectionShuffleService> _logger;
    private readonly PluginConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="CollectionShuffleService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="configuration">The plugin configuration.</param>
    public CollectionShuffleService(
        ILogger<CollectionShuffleService> logger,
        PluginConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Shuffles a collection of items with the configured algorithm.
    /// </summary>
    /// <param name="items">The items to shuffle.</param>
    /// <returns>A shuffled collection of items.</returns>
    public ICollection<object> ShuffleItems(ICollection<object> items)
    {
        if (!_configuration.EnableCollectionShuffle)
        {
            _logger.LogDebug("Collection shuffle is disabled, returning original order");
            return items;
        }

        try
        {
            if (items.Count == 0)
            {
                _logger.LogWarning("No items to shuffle");
                return items;
            }

            _logger.LogInformation("Shuffling {ItemCount} items", items.Count);

            var shuffledItems = ShuffleItemsInternal(items);

            _logger.LogInformation("Successfully shuffled {ItemCount} items", items.Count);
            return shuffledItems;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error shuffling items");
            // Fall back to original order on error
            return items;
        }
    }

    private ICollection<object> ShuffleItemsInternal(ICollection<object> items)
    {
        if (items.Count <= 1)
        {
            return items;
        }

        var random = new Random();
        var itemList = new List<object>(items);

        // Apply the configured shuffle algorithm
        if (_configuration.ShuffleShowsFirst)
        {
            itemList = ShuffleShowsFirst(itemList, random);
        }

        if (_configuration.ShuffleEpisodesSecond)
        {
            itemList = ShuffleEpisodesSecond(itemList, random);
        }

        if (_configuration.PreventBackToBackShows)
        {
            itemList = PreventBackToBackShows(itemList, random);
        }

        return itemList;
    }

    private List<object> ShuffleShowsFirst(List<object> items, Random random)
    {
        // For now, implement a simple shuffle that can be extended later
        // In a full implementation, this would group by series and shuffle the series order
        var shuffledList = new List<object>(items);

        // Fisher-Yates shuffle
        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            var temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }

        _logger.LogDebug("Applied show-level shuffle to {ItemCount} items", items.Count);
        return shuffledList;
    }

    private List<object> ShuffleEpisodesSecond(List<object> items, Random random)
    {
        // For now, implement a simple shuffle that can be extended later
        // In a full implementation, this would shuffle episodes within each series
        var shuffledList = new List<object>(items);

        // Fisher-Yates shuffle
        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            var temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }

        _logger.LogDebug("Applied episode-level shuffle to {ItemCount} items", items.Count);
        return shuffledList;
    }

    private List<object> PreventBackToBackShows(List<object> items, Random random)
    {
        if (items.Count <= 1)
        {
            return items;
        }

        var result = new List<object>();
        var maxConsecutive = Math.Max(1, _configuration.MaxConsecutiveShowsFromSameSeries);
        var remainingItems = new List<object>(items);
        var lastSeries = string.Empty;
        var consecutiveCount = 0;

        while (remainingItems.Count > 0)
        {
            // For now, treat each item as its own series
            // In a full implementation, this would extract series information from the items
            var currentSeries = remainingItems[0].ToString() ?? "Unknown";

            // Check if adding this item would exceed consecutive limit
            if (currentSeries == lastSeries && consecutiveCount >= maxConsecutive)
            {
                // Find an item from a different series
                var differentSeriesItem = remainingItems.FirstOrDefault(item =>
                    (item.ToString() ?? "Unknown") != lastSeries);

                if (differentSeriesItem != null)
                {
                    // Move the different series item to the front
                    remainingItems.Remove(differentSeriesItem);
                    remainingItems.Insert(0, differentSeriesItem);
                    currentSeries = differentSeriesItem.ToString() ?? "Unknown";
                }
            }

            var itemToAdd = remainingItems[0];
            remainingItems.RemoveAt(0);

            result.Add(itemToAdd);

            if (currentSeries == lastSeries)
            {
                consecutiveCount++;
            }
            else
            {
                lastSeries = currentSeries;
                consecutiveCount = 1;
            }
        }

        _logger.LogDebug("Applied back-to-back prevention with max consecutive: {MaxConsecutive}", maxConsecutive);
        return result;
    }
}