#!/bin/bash

# Prepare release script for Jellyfin Collection Shuffle Plugin
# This script builds the plugin and prepares it for distribution

set -e

echo "Preparing Jellyfin Collection Shuffle Plugin release..."

# Build the plugin
echo "Building plugin..."
dotnet build --configuration Release --no-restore

# Check if build was successful
if [ $? -ne 0 ]; then
    echo "Build failed! Exiting."
    exit 1
fi

# Create release directory
RELEASE_DIR="release"
PLUGIN_NAME="Jellyfin.Plugin.CollectionShuffle"
VERSION="1.0.0"

echo "Creating release directory..."
rm -rf "$RELEASE_DIR"
mkdir -p "$RELEASE_DIR"

# Copy plugin files
echo "Copying plugin files..."
cp "Jellyfin.Plugin.Template/bin/Release/net8.0/Jellyfin.Plugin.Template.dll" "$RELEASE_DIR/"
cp "Jellyfin.Plugin.Template/bin/Release/net8.0/Jellyfin.Plugin.Template.deps.json" "$RELEASE_DIR/"
cp "Jellyfin.Plugin.Template/bin/Release/net8.0/Jellyfin.Plugin.Template.xml" "$RELEASE_DIR/"

# Create zip file
echo "Creating release zip..."
cd "$RELEASE_DIR"
zip -r "../${PLUGIN_NAME}-v${VERSION}.zip" .
cd ..

# Calculate MD5 checksum
echo "Calculating MD5 checksum..."
CHECKSUM=$(md5sum "${PLUGIN_NAME}-v${VERSION}.zip" | cut -d' ' -f1)
echo "MD5 Checksum: $CHECKSUM"

# Update manifest with checksum
echo "Updating manifest with checksum..."
sed -i "s/\"checksum\": \"[^\"]*\"/\"checksum\": \"$CHECKSUM\"/" manifest.json

# Create final manifest
echo "Creating final manifest..."
cp manifest.json "${PLUGIN_NAME}-manifest.json"

echo ""
echo "Release preparation complete!"
echo "Files created:"
echo "  - ${PLUGIN_NAME}-v${VERSION}.zip (plugin binary)"
echo "  - ${PLUGIN_NAME}-manifest.json (manifest with checksum)"
echo ""
echo "Plugin features:"
echo "  ✓ Configuration screen with enable/disable options"
echo "  ✓ Advanced shuffle algorithms (shows first, then episodes)"
echo "  ✓ Back-to-back prevention for episodes from same series"
echo "  ✓ Comprehensive logging and debugging"
echo "  ✓ Fallback safety mechanisms"
echo ""
echo "Next steps:"
echo "1. Upload the zip file to your GitHub releases or hosting service"
echo "2. Update the sourceUrl in the manifest to point to your actual download URL"
echo "3. Host the manifest.json file at a publicly accessible URL"
echo "4. Add the manifest URL to Jellyfin: Dashboard → Plugins → Repositories"
echo ""
echo "Note: Update the 'owner' field in manifest.json with your actual username/organization"
echo ""
echo "To test the configuration form, open test-config.html in a web browser"