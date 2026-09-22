$GameDir = "D:\SteamLibrary\steamapps\common\RiseOfIndustry"
$CscPath = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe"
$OutputDll = "AlwaysNightMod.dll"

# Paths to required DLLs
$BepInExDir = Join-Path $GameDir "BepInEx\core"
$ManagedDir = Join-Path $GameDir "Rise of Industry_Data\Managed"

$References = @(
    (Join-Path $BepInExDir "BepInEx.dll"),
    (Join-Path $BepInExDir "0Harmony.dll"),
    (Join-Path $ManagedDir "UnityEngine.dll"),
    (Join-Path $ManagedDir "UnityEngine.CoreModule.dll"),
    (Join-Path $ManagedDir "Assembly-CSharp.dll")
)

# Check if references exist
foreach ($Ref in $References) {
    if (-not (Test-Path $Ref)) {
        Write-Host "Error: Cannot find required reference: $Ref" -ForegroundColor Red
        Write-Host "Please ensure BepInEx is installed and the GameDir is correct."
        exit 1
    }
}

# Construct reference string
$RefString = $References | ForEach-Object { "/reference:""$_""" }
$RefArgs = $RefString -join " "

Write-Host "Compiling AlwaysNightMod.dll..."
$Command = "& ""$CscPath"" /nologo /target:library /out:""$OutputDll"" $RefArgs AlwaysNightMod.cs"

Invoke-Expression $Command

if ($LASTEXITCODE -eq 0) {
    Write-Host "Compilation successful!" -ForegroundColor Green
    
    $PluginsDir = Join-Path $GameDir "BepInEx\plugins"
    if (-not (Test-Path $PluginsDir)) {
        New-Item -ItemType Directory -Path $PluginsDir -Force | Out-Null
    }
    
    Copy-Item $OutputDll -Destination $PluginsDir -Force
    Write-Host "Successfully copied to $PluginsDir\$OutputDll" -ForegroundColor Cyan
} else {
    Write-Host "Compilation failed." -ForegroundColor Red
}
