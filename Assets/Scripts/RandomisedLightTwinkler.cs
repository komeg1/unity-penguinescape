using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RandomisedLightTwinkler : MonoBehaviour
{
    [SerializeField] float maxIntensity = 1f;
    [SerializeField] float minIntensity = 0.2f;

    [SerializeField] float maxChangeSpeed = 0.1f;
    [SerializeField] float maxChangeSpeedDeviation = 0.1f;


    private Light2D light;

    private void Start()
    {
        light= GetComponent<Light2D>();
    }

    void Update()
    {
        float change = Random.Range(PlusMinus(maxChangeSpeed, maxChangeSpeedDeviation),
            -PlusMinus(maxChangeSpeed, maxChangeSpeedDeviation));
        light.intensity = Mathf.Clamp(light.intensity + change, minIntensity, maxIntensity);
    }

    float sine01(float value, float valuefor1)
    {
        return Mathf.Abs(Mathf.Sin(value / valuefor1 * Mathf.PI));
    }

    public float PlusMinus(float value, float deviation)
    {
        return Random.Range(value - deviation, value + deviation);
    }
}
