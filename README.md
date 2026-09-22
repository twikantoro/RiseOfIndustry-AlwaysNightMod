# Rise of Industry - Always Night Mod

A simple visual mod for *Rise of Industry* that permanently sets the game's lighting to a nighttime atmosphere.

## Features
- Forces the main directional light (Sun) to a low-intensity dark blue color.
- Adjusts the global `RenderSettings` (ambient light and reflections) to match the nighttime environment.
- Safely patches the game in-memory using **BepInEx** and **Harmony**, meaning your original game files are untouched and safe from game updates.

## Requirements
- [Rise of Industry](https://store.steampowered.com/app/671440/Rise_of_Industry/)
- [BepInEx (x64) v5.4.22+](https://github.com/BepInEx/BepInEx/releases)

## Installation

1. Download and extract **BepInEx x64** into your `Rise of Industry` root folder (where `Rise of Industry.exe` is located).
2. Run the game once to allow BepInEx to generate its configuration files and folders, then close the game.
3. Download the `AlwaysNightMod.dll` from the [Releases](../../releases) page (or compile it yourself).
4. Place `AlwaysNightMod.dll` into the `BepInEx\plugins` folder inside your game directory.
5. Launch the game and enjoy the permanent night!

## Compiling from Source

If you want to compile the mod yourself, a PowerShell build script is included. 

1. Edit the `$GameDir` variable in `build.ps1` to match your *Rise of Industry* installation path.
2. Ensure you have the .NET Framework compiler (`csc.exe`) available. The script defaults to `C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe`.
3. Open PowerShell and run:
   ```powershell
   .\build.ps1
   ```
4. The script will automatically compile `AlwaysNightMod.dll` and copy it to your `BepInEx\plugins` folder.

## License
MIT License
