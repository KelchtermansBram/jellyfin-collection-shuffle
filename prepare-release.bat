@echo off
setlocal enabledelayedexpansion

echo Preparing Jellyfin Collection Shuffle Plugin release...

REM Build the plugin
echo Building plugin...
dotnet build --configuration Release --no-restore

REM Check if build was successful
if %ERRORLEVEL% neq 0 (
    echo Build failed! Exiting.
    pause
    exit /b 1
)

REM Create release directory
set RELEASE_DIR=release
set PLUGIN_NAME=Jellyfin.Plugin.CollectionShuffle
set VERSION=1.0.0

echo Creating release directory...
if exist "%RELEASE_DIR%" rmdir /s /q "%RELEASE_DIR%"
mkdir "%RELEASE_DIR%"

REM Copy plugin files
echo Copying plugin files...
copy "Jellyfin.Plugin.Template\bin\Release\net8.0\Jellyfin.Plugin.Template.dll" "%RELEASE_DIR%\"
copy "Jellyfin.Plugin.Template\bin\Release\net8.0\Jellyfin.Plugin.Template.deps.json" "%RELEASE_DIR%\"
copy "Jellyfin.Plugin.Template\bin\Release\net8.0\Jellyfin.Plugin.Template.xml" "%RELEASE_DIR%\"

REM Create zip file (requires PowerShell)
echo Creating release zip...
powershell -Command "Compress-Archive -Path '%RELEASE_DIR%\*' -DestinationPath '%PLUGIN_NAME%-v%VERSION%.zip' -Force"

REM Note: MD5 checksum calculation requires additional tools on Windows
echo.
echo Release preparation complete!
echo Files created:
echo   - %PLUGIN_NAME%-v%VERSION%.zip (plugin binary)
echo.
echo Next steps:
echo 1. Upload the zip file to your GitHub releases or hosting service
echo 2. Calculate MD5 checksum of the zip file
echo 3. Update the manifest.json with the checksum and correct sourceUrl
echo 4. Host the manifest.json file at a publicly accessible URL
echo 5. Add the manifest URL to Jellyfin: Dashboard → Plugins → Repositories
echo.
echo Note: Update the 'owner' field in manifest.json with your actual username/organization
echo.
echo To calculate MD5 checksum on Windows, you can use:
echo   - PowerShell: Get-FileHash -Algorithm MD5 "%PLUGIN_NAME%-v%VERSION%.zip"
echo   - Or use online MD5 calculators
echo.
pause