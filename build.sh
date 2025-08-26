#!/bin/bash

# Build script for Jellyfin Collection Shuffle Plugin

echo "Building Jellyfin Collection Shuffle Plugin..."

# Clean previous builds
echo "Cleaning previous builds..."
dotnet clean

# Restore packages
echo "Restoring packages..."
dotnet restore

# Build in Release mode
echo "Building in Release mode..."
dotnet build --configuration Release --no-restore

# Check if build was successful
if [ $? -eq 0 ]; then
    echo "Build successful!"
    echo "Plugin files are located in: Jellyfin.Plugin.Template/bin/Release/net8.0/"
    echo ""
    echo "To install the plugin:"
    echo "1. Copy the .dll file to your Jellyfin plugins directory"
    echo "2. Restart Jellyfin Server"
    echo "3. Configure the plugin in Admin → Plugins → Collection Shuffle"
else
    echo "Build failed!"
    exit 1
fi