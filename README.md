# Rise of Industry - Always Night Mod

A native, zero-overhead visual mod for *Rise of Industry* that permanently sets the game's lighting to a nighttime atmosphere.

> **Tested on Game Version:** `2.3.3 : 0507b*` (Steam - Public)

## Screenshots
![Always Night View 1](assets/screenshot1.png)
![Always Night View 2](assets/screenshot2.png)

## Features
- Forces the main directional light (Sun) to a low-intensity, cinematic moonlight blue color.
- Adjusts the global `RenderSettings` (ambient light and reflections) to perfectly match the nighttime environment.
- Native IL code injection using `Mono.Cecil` guarantees perfect stability, with zero loading screen freezes.

> ⚠️ **Disclaimer:** Because this mod drastically lowers the ambient and directional light to create a nighttime atmosphere, raw natural resources in the world (like oil and coal) will blend into the terrain and become barely visible. Luckily, you can easily use the game's built-in labels and resource overlays to find them!

## Installation (For Players)
The easiest way to install this mod is to use the pre-compiled Release:
1. Download the latest `AlwaysNightMod_vX.X.X.zip` from the [Releases](../../releases) page.
2. Extract the contents directly into your main *Rise of Industry* game folder (the folder containing `Rise of Industry.exe`).
3. Ensure the game is closed.
4. Double-click `Install_Mod.bat`. 
5. (A backup of your original game logic is automatically created as `Assembly-CSharp.dll.bak` in case you ever want to revert).
6. Launch the game and enjoy the night!

## Compiling from Source (For Developers)
If you want to modify the source code or compile the mod yourself:
1. Clone this repository.
2. Ensure your game is closed.
3. Open `build.ps1` and verify `$GameDir` points to your game folder.
4. Run `build.ps1` to compile the hook DLL and apply the Mono.Cecil patch.
