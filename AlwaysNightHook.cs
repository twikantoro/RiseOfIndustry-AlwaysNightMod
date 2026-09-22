using System;
using UnityEngine;

public class AlwaysNightBehavior : MonoBehaviour
{
    private float _timer = 0f;
    private Light[] _allLights;

    void Start()
    {
        Debug.Log("AlwaysNightBehavior Started!");
    }

    void LateUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer > 2f)
        {
            _timer = 0f;
            _allLights = FindObjectsOfType<Light>();
        }

        if (_allLights != null)
        {
            foreach (var light in _allLights)
            {
                if (light.type == LightType.Directional)
                {
                    light.color = new Color(0.05f, 0.1f, 0.25f, 1.0f);
                    light.intensity = 0.1f;
                    light.shadowStrength = 0.8f;
                }
            }
        }

        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.02f, 0.02f, 0.08f, 1.0f);
        RenderSettings.ambientIntensity = 0.1f;
        RenderSettings.reflectionIntensity = 0.05f;
    }
}

public static class AlwaysNightHook
{
    private static bool _initialized = false;

    public static void Init()
    {
        try
        {
            if (!_initialized)
            {
                var go = new GameObject("AlwaysNightEnforcer");
                UnityEngine.Object.DontDestroyOnLoad(go);
                go.AddComponent<AlwaysNightBehavior>();
                _initialized = true;
            }
        }
        catch (Exception)
        {
        }
    }
}
