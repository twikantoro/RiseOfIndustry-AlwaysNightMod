$GameDir = "D:\SteamLibrary\steamapps\common\RiseOfIndustry"
$CscPath = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe"
$ManagedDir = Join-Path $GameDir "Rise of Industry_Data\Managed"

Write-Host "Compiling AlwaysNightHook.dll..."
$HookCommand = "& ""$CscPath"" /nologo /target:library /out:""$ManagedDir\AlwaysNightHook.dll"" /reference:""$ManagedDir\UnityEngine.dll"" /reference:""$ManagedDir\UnityEngine.CoreModule.dll"" /reference:""$ManagedDir\Assembly-CSharp.dll"" AlwaysNightHook.cs"
Invoke-Expression $HookCommand

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to compile hook!" -ForegroundColor Red
    exit 1
}

Write-Host "Compiling Patcher..."
$PatcherCommand = "& ""$CscPath"" /nologo /out:Patcher.exe /reference:Mono.Cecil.dll Patcher.cs"
Invoke-Expression $PatcherCommand

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to compile patcher!" -ForegroundColor Red
    exit 1
}

Write-Host "Running Patcher..."
.\Patcher.exe $GameDir

Write-Host "Mod installation complete!" -ForegroundColor Green
