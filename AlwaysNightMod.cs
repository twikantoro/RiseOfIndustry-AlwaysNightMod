using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using ProjectAutomata;

namespace AlwaysNightMod
{
    [BepInPlugin("com.antigravity.alwaysnight", "Always Night", "1.0.0")]
    public class AlwaysNightPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            Logger.LogInfo("Always Night Mod Loading...");
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

    [HarmonyPatch(typeof(Sun), "Update")]
    public class SunUpdatePatch
    {
        static void Postfix(Sun __instance)
        {
            var lightProp = __instance.GetType().GetProperty("light", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (lightProp != null)
            {
                Light light = lightProp.GetValue(__instance, null) as Light;
                if (light != null)
                {
                    light.color = new Color(0.1f, 0.15f, 0.3f, 1.0f); // Dark blue for night
                    light.intensity = 0.15f; // Low intensity
                }
            }
            
            RenderSettings.ambientLight = new Color(0.05f, 0.05f, 0.1f, 1.0f);
            RenderSettings.ambientIntensity = 0.2f;
            RenderSettings.reflectionIntensity = 0.1f;
        }
    }

    [HarmonyPatch(typeof(Sun), "UpdateParmaters")]
    public class SunUpdateParametersPatch
    {
        static void Postfix(Sun __instance)
        {
            var lightProp = __instance.GetType().GetProperty("light", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            if (lightProp != null)
            {
                Light light = lightProp.GetValue(__instance, null) as Light;
                if (light != null)
                {
                    light.color = new Color(0.1f, 0.15f, 0.3f, 1.0f); // Dark blue for night
                    light.intensity = 0.15f; // Low intensity
                }
            }
        }
    }
}
