# Installation Guide for Jellyfin Collection Shuffle Plugin

## Prerequisites

- Jellyfin Server 10.8.0 or later
- .NET 8.0 runtime (usually included with Jellyfin)

## Installation Steps

### Method 1: Build from Source (Recommended for Development)

1. **Clone the repository:**

   ```bash
   git clone https://github.com/yourusername/jellyfin-collection-shuffle.git
   cd jellyfin-collection-shuffle
   ```

2. **Build the plugin:**

   ```bash
   # On Linux/macOS:
   ./build.sh

   # On Windows:
   build.bat
   ```

3. **Install the plugin:**

   - Copy the generated `Jellyfin.Plugin.Template.dll` from `Jellyfin.Plugin.Template/bin/Release/net8.0/`
   - Paste it into your Jellyfin plugins directory:
     - **Windows**: `%PROGRAMDATA%\Jellyfin\Server\plugins\CollectionShuffle\`
     - **Linux**: `/var/lib/jellyfin/plugins/CollectionShuffle/`
     - **Docker**: Mount the plugins directory to your container

4. **Restart Jellyfin Server**

### Method 2: Download Pre-built Release

1. **Download the latest release** from the GitHub releases page
2. **Extract the files** to your Jellyfin plugins directory
3. **Restart Jellyfin Server**

## Configuration

1. **Access the plugin configuration:**

   - Open Jellyfin Dashboard
   - Go to Admin → Plugins
   - Find "Collection Shuffle" and click on it

2. **Configure the plugin:**

   - **Enable Collection Shuffle**: Master toggle for the plugin
   - **Prevent Back-to-Back Shows**: Ensures episodes from the same series don't play consecutively
   - **Shuffle Shows First**: Randomizes the order of different series
   - **Shuffle Episodes Second**: Randomizes episodes within each series
   - **Max Consecutive Shows from Same Series**: Maximum consecutive episodes from the same show (minimum: 1)

3. **Save the configuration**

## Usage

1. **Create or use an existing collection** in Jellyfin
2. **Add TV shows, movies, or other media** to the collection
3. **Use the shuffle button** on the collection
4. **The plugin will automatically apply** the configured shuffle algorithm

## Verification

To verify the plugin is working:

1. **Check the plugin status** in Admin → Plugins
2. **Look for plugin logs** in the Jellyfin server logs
3. **Test with a collection** containing multiple items from different series

## Troubleshooting

### Plugin Not Appearing

- Ensure the `.dll` file is in the correct plugins subdirectory
- Check that Jellyfin Server has been restarted
- Verify the plugin file permissions

### Shuffle Not Working

- Check that "Enable Collection Shuffle" is enabled in the configuration
- Verify the collection contains multiple items
- Check Jellyfin server logs for any error messages

### Performance Issues

- The plugin adds minimal overhead to shuffle operations
- Large collections may take slightly longer to process
- Consider adjusting configuration options if needed

## Uninstallation

1. **Disable the plugin** in Admin → Plugins
2. **Remove the plugin files** from the plugins directory
3. **Restart Jellyfin Server**

## Support

If you encounter issues:

1. Check the [GitHub Issues](https://github.com/yourusername/jellyfin-collection-shuffle/issues) page
2. Create a new issue with:
   - Jellyfin version
   - Plugin version
   - Operating system
   - Error messages or logs
   - Steps to reproduce the issue

## Development

For developers who want to contribute:

1. **Fork the repository**
2. **Create a feature branch**
3. **Make your changes**
4. **Test thoroughly**
5. **Submit a pull request**

See the main README.md for more development details.
