using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightFlicker : MonoBehaviour
{
    Light2D light;

    public float minInnerRadius, maxInnerRadius;
    public float minOuterRadius, maxOuterRadius;
    public float minIntensity, maxIntensity;
    public Color[] colours;
    public float timePerCycleMin, timePerCycleMax;

    float innerRadius, outerRadius, intensity;
    float targetInnerRadius, targetOuterRadius, targetIntensity;
    Color targetColour;

    float elapsedTimer;
    float nextCycle;
    Color colour;

    private void Awake()
    {
        light = GetComponent<Light2D>();
    }

    private void Start()
    {
        // Set initial value variables
        innerRadius = Random.Range(minInnerRadius, maxInnerRadius);
        outerRadius = Random.Range(maxOuterRadius, maxOuterRadius);
        intensity = Random.Range(minIntensity, maxIntensity);
        nextCycle = Random.Range(timePerCycleMin, timePerCycleMax);
        colour = colours[0];
        targetColour = colours[1];

        // Set target value variables
        ResetTargetValues();

        // Set initial light properties
        light.pointLightInnerRadius = innerRadius;
        light.pointLightOuterRadius = outerRadius;
        light.intensity = intensity;
        light.color = colour;

    }

    private void ResetTargetValues()
    {
        elapsedTimer = 0;
        nextCycle = Random.Range(timePerCycleMin, timePerCycleMax);

        innerRadius = targetInnerRadius;
        outerRadius = targetOuterRadius;
        intensity = targetIntensity;
        colour = targetColour;

        targetInnerRadius = Random.Range(minInnerRadius, maxInnerRadius);
        targetOuterRadius = Random.Range(minOuterRadius, maxOuterRadius);
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        targetColour = colours[Random.Range(0, colours.Length - 1)];
    }

    private void Update()
    {
        if (elapsedTimer < nextCycle)
        {
            // Size settings
            light.pointLightInnerRadius = Mathf.Lerp(innerRadius, targetInnerRadius, elapsedTimer / nextCycle);
            light.pointLightOuterRadius = Mathf.Lerp(outerRadius, targetOuterRadius, elapsedTimer / nextCycle);

            // Intensity settings
            light.intensity = Mathf.Lerp(intensity, targetIntensity, elapsedTimer / nextCycle);

            // Colour settings
            light.color = Color.Lerp(colour, targetColour, elapsedTimer / nextCycle);
            elapsedTimer += Time.deltaTime;
        }
        else
        {
            ResetTargetValues();
        }
    }
}
