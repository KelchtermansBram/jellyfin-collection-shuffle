using System;
using System.Collections.Generic;
using System.Globalization;
using Jellyfin.Plugin.CollectionShuffle.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.CollectionShuffle;

/// <summary>
/// The main plugin for collection shuffle functionality.
/// </summary>
public class Plugin : IPlugin
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Plugin"/> class.
    /// </summary>
    public Plugin()
    {
        Instance = this;
        Configuration = new PluginConfiguration();
    }

    /// <summary>
    /// Gets the plugin name.
    /// </summary>
    public string Name => "Collection Shuffle";

    /// <summary>
    /// Gets the plugin description.
    /// </summary>
    public string Description => "Overrides Jellyfin's shuffle functionality for collections to randomize shows first, then episodes, while preventing back-to-back episodes from the same show.";

    /// <summary>
    /// Gets the plugin ID.
    /// </summary>
    public Guid Id => Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");

    /// <summary>
    /// Gets the plugin version.
    /// </summary>
    public Version Version => new Version(1, 0, 0);

    /// <summary>
    /// Gets the assembly file path.
    /// </summary>
    public string AssemblyFilePath => string.Empty;

    /// <summary>
    /// Gets the data folder path.
    /// </summary>
    public string DataFolderPath => string.Empty;

    /// <summary>
    /// Gets a value indicating whether the plugin can be uninstalled.
    /// </summary>
    public bool CanUninstall => true;

    /// <summary>
    /// Gets the current plugin instance.
    /// </summary>
    public static Plugin? Instance { get; private set; }

    /// <summary>
    /// Gets the plugin configuration.
    /// </summary>
    public PluginConfiguration Configuration { get; }

    /// <summary>
    /// Gets the plugin pages.
    /// </summary>
    /// <returns>A collection of plugin page information.</returns>
    public IEnumerable<PluginPageInfo> GetPages()
    {
        return
        [
            new PluginPageInfo
            {
                Name = Name,
                EmbeddedResourcePath = string.Format(CultureInfo.InvariantCulture, "{0}.Configuration.configPage.html", GetType().Namespace)
            }
        ];
    }

    /// <summary>
    /// Gets plugin information.
    /// </summary>
    /// <returns>Plugin information.</returns>
    public PluginInfo GetPluginInfo()
    {
        return new PluginInfo(Name, Version, Description, Id, CanUninstall);
    }

    /// <summary>
    /// Called when the plugin is being uninstalled.
    /// </summary>
    public void OnUninstalling()
    {
        // Cleanup code if needed
    }
}
