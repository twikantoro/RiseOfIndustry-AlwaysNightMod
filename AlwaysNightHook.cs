using System;
using UnityEngine;
using ProjectAutomata;

public static class AlwaysNightHook
{
    private static bool _nightSet = false;

    public static void ApplyNight(Sun sun)
    {
        try
        {
            if (sun != null && sun.light != null)
            {
                sun.light.color = new Color(0.1f, 0.15f, 0.3f, 1.0f);
                sun.light.intensity = 0.15f;
            }

            if (!_nightSet)
            {
                RenderSettings.ambientLight = new Color(0.05f, 0.05f, 0.1f, 1.0f);
                RenderSettings.ambientIntensity = 0.2f;
                RenderSettings.reflectionIntensity = 0.1f;
                _nightSet = true;
            }
        }
        catch (Exception)
        {
        }
    }
}
