using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;
    
    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    public TimeController timeController;

    private void Awake()
    {
        timeController = GetComponent<TimeController>();
    }

    private void Update()
    {
        UpdateLighting(sun, sunColor, sunIntensity);
        
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(timeController.time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(timeController.time);
    }

    private void UpdateLighting(Light lightSource, Gradient gradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(timeController.time);

        lightSource.transform.eulerAngles = (timeController.time - 0.25f) * timeController.noon * 4f;
        lightSource.color = gradient.Evaluate(timeController.time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        }
        else if (lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
