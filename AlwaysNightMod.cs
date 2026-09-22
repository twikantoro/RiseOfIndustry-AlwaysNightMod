using System;
using BepInEx;
using UnityEngine;
using ProjectAutomata;

namespace AlwaysNightMod
{
    [BepInPlugin("com.antigravity.alwaysnight", "Always Night", "1.0.2")]
    public class AlwaysNightPlugin : BaseUnityPlugin
    {
        private Sun _cachedSun;

        void Awake()
        {
            Logger.LogInfo("Always Night Mod v1.0.2 Loading (Harmony-Free)...");
        }

        void LateUpdate()
        {
            try
            {
                if (_cachedSun == null)
                {
                    _cachedSun = UnityEngine.Object.FindObjectOfType<Sun>();
                }

                if (_cachedSun != null && _cachedSun.light != null)
                {
                    _cachedSun.light.color = new Color(0.05f, 0.1f, 0.25f, 1.0f); // Dark blue
                    _cachedSun.light.intensity = 0.15f;
                    
                    RenderSettings.ambientLight = new Color(0.02f, 0.02f, 0.08f, 1.0f);
                    RenderSettings.ambientIntensity = 0.2f;
                    RenderSettings.reflectionIntensity = 0.1f;
                }
            }
            catch (Exception)
            {
                // Silent catch to prevent spam
            }
        }
    }
}
