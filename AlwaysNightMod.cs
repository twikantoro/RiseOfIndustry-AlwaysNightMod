using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using ProjectAutomata;

namespace AlwaysNightMod
{
    [BepInPlugin("com.antigravity.alwaysnight", "Always Night", "1.0.1")]
    public class AlwaysNightPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            Logger.LogInfo("Always Night Mod Loading v1.0.1...");
            try 
            {
                var harmony = new Harmony("com.antigravity.alwaysnight");
                harmony.PatchAll();
                Logger.LogInfo("Always Night Mod Patches Applied Successfully!");
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to patch: " + ex);
            }
        }
    }

    [HarmonyPatch(typeof(Sun), "UpdateParmaters")]
    public class SunUpdateParametersPatch
    {
        static void Postfix(Sun __instance)
        {
            try
            {
                // Accessing the field directly if we can, or property
                if (__instance != null && __instance.light != null)
                {
                    __instance.light.color = new Color(0.1f, 0.15f, 0.3f, 1.0f);
                    __instance.light.intensity = 0.15f;
                }
                
                RenderSettings.ambientLight = new Color(0.05f, 0.05f, 0.1f, 1.0f);
                RenderSettings.ambientIntensity = 0.2f;
                RenderSettings.reflectionIntensity = 0.1f;
            }
            catch (Exception ex)
            {
                // Suppress to avoid log spam/freezes
            }
        }
    }
    
    [HarmonyPatch(typeof(Sun), "Update")]
    public class SunUpdatePatch
    {
        static void Postfix(Sun __instance)
        {
            try
            {
                if (__instance != null && __instance.light != null)
                {
                    __instance.light.color = new Color(0.1f, 0.15f, 0.3f, 1.0f);
                    __instance.light.intensity = 0.15f;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
