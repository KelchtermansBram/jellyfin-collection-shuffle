# Jellyfin Collection Shuffle Plugin

A Jellyfin plugin that overrides the default shuffle functionality for collections, providing intelligent randomization of shows and episodes while preventing back-to-back episodes from the same series.

## Features

- **Smart Show Shuffling**: Randomizes the order of shows first, then episodes within each show
- **Back-to-Back Prevention**: Ensures episodes from the same series don't play consecutively
- **Configurable Behavior**: Multiple options to customize the shuffle algorithm
- **Fallback Safety**: Gracefully falls back to Jellyfin's default behavior if errors occur
- **Collection Support**: Works with Collections, Playlists, and Folders

## How It Works

The plugin implements a two-stage shuffle algorithm:

1. **Show-Level Shuffling**: Randomizes the order of different TV series or movies
2. **Episode-Level Shuffling**: Randomizes episodes within each individual series
3. **Back-to-Back Prevention**: Ensures no more than the configured number of consecutive episodes from the same series

This approach provides variety while maintaining logical grouping, preventing the jarring experience of jumping between completely different shows.

## Configuration Options

- **Enable Collection Shuffle**: Master toggle to enable/disable the plugin
- **Prevent Back-to-Back Shows**: Ensures episodes from the same series don't play consecutively
- **Shuffle Shows First**: Randomizes the order of different series before shuffling episodes
- **Shuffle Episodes Second**: Randomizes episodes within each individual series
- **Max Consecutive Shows from Same Series**: Maximum number of consecutive episodes allowed from the same show (minimum: 1)

## Installation

1. Download the latest release from the releases page
2. Place the `.dll` file in your Jellyfin plugins directory:
   - **Windows**: `%PROGRAMDATA%\Jellyfin\Server\plugins`
   - **Linux**: `/var/lib/jellyfin/plugins`
   - **Docker**: Mount the plugins directory to your container
3. Restart Jellyfin Server
4. Navigate to Admin → Plugins → Collection Shuffle to configure

## Building from Source

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code with C# extension

### Build Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/jellyfin-collection-shuffle.git
   cd jellyfin-collection-shuffle
   ```

2. Build the project:

   ```bash
   dotnet build --configuration Release
   ```

3. The compiled plugin will be in `Jellyfin.Plugin.Template/bin/Release/net8.0/`

## Usage

Once installed and configured:

1. Create or use an existing collection in Jellyfin
2. Add TV shows, movies, or other media to the collection
3. Use the shuffle button on the collection
4. The plugin will automatically apply the configured shuffle algorithm

## How It Differs from Default Jellyfin Shuffle

**Default Jellyfin Behavior:**

- Simple random shuffle of all items
- No consideration for series grouping
- Episodes from the same show can play consecutively

**With This Plugin:**

- Intelligent grouping by series
- Controlled randomization that maintains variety
- Configurable back-to-back prevention
- Better user experience for TV show collections

## Troubleshooting

### Plugin Not Working

- Ensure the plugin is enabled in the configuration
- Check Jellyfin server logs for any error messages
- Verify the plugin is properly installed in the plugins directory

### Shuffle Still Using Default Behavior

- Check that "Enable Collection Shuffle" is checked in the plugin configuration
- Restart Jellyfin server after configuration changes
- Verify the collection contains multiple items

### Performance Issues

- The plugin adds minimal overhead to shuffle operations
- Large collections may take slightly longer to process
- Consider adjusting configuration options if needed

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Development Guidelines

- Follow the existing code style and patterns
- Add appropriate logging for debugging
- Include error handling and fallback behavior
- Test with various collection types and sizes

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

If you encounter any issues or have questions:

1. Check the [Issues](https://github.com/yourusername/jellyfin-collection-shuffle/issues) page
2. Create a new issue with detailed information about your problem
3. Include Jellyfin version, plugin version, and any error messages

## Changelog

### Version 1.0.0

- Initial release
- Basic collection shuffle functionality
- Configuration options for shuffle behavior
- Back-to-back prevention algorithm
