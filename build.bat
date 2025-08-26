@echo off
echo Building Jellyfin Collection Shuffle Plugin...

REM Clean previous builds
echo Cleaning previous builds...
dotnet clean

REM Restore packages
echo Restoring packages...
dotnet restore

REM Build in Release mode
echo Building in Release mode...
dotnet build --configuration Release --no-restore

REM Check if build was successful
if %ERRORLEVEL% EQU 0 (
    echo Build successful!
    echo Plugin files are located in: Jellyfin.Plugin.Template\bin\Release\net8.0\
    echo.
    echo To install the plugin:
    echo 1. Copy the .dll file to your Jellyfin plugins directory
    echo 2. Restart Jellyfin Server
    echo 3. Configure the plugin in Admin → Plugins → Collection Shuffle
) else (
    echo Build failed!
    pause
    exit /b 1
)

pause