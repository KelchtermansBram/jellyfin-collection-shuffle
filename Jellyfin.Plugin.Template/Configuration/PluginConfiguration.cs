using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Jellyfin.Plugin.CollectionShuffle.Configuration;

/// <summary>
/// Plugin configuration for collection shuffle.
/// </summary>
public class PluginConfiguration
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PluginConfiguration"/> class.
    /// </summary>
    public PluginConfiguration()
    {
        // Set default values
        EnableCollectionShuffle = true;
        PreventBackToBackShows = true;
        ShuffleShowsFirst = true;
        ShuffleEpisodesSecond = true;
        MaxConsecutiveShowsFromSameSeries = 1;
    }

    /// <summary>
    /// Gets or sets a value indicating whether collection shuffle is enabled.
    /// </summary>
    [DataMember]
    [DisplayName("Enable Collection Shuffle")]
    [Description("Enable or disable the collection shuffle functionality")]
    public bool EnableCollectionShuffle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to prevent back-to-back shows from the same series.
    /// </summary>
    [DataMember]
    [DisplayName("Prevent Back-to-Back Shows")]
    [Description("Prevent episodes from the same show from playing consecutively")]
    public bool PreventBackToBackShows { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to shuffle shows first.
    /// </summary>
    [DataMember]
    [DisplayName("Shuffle Shows First")]
    [Description("Randomize the order of shows before shuffling episodes")]
    public bool ShuffleShowsFirst { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to shuffle episodes second.
    /// </summary>
    [DataMember]
    [DisplayName("Shuffle Episodes Second")]
    [Description("Randomize the order of episodes within each show")]
    public bool ShuffleEpisodesSecond { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of consecutive episodes from the same series.
    /// </summary>
    [DataMember]
    [DisplayName("Max Consecutive Shows from Same Series")]
    [Description("Maximum number of consecutive episodes allowed from the same show (minimum: 1)")]
    public int MaxConsecutiveShowsFromSameSeries { get; set; }
}
