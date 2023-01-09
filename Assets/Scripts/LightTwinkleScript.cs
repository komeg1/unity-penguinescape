using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LightTwinkleScript : MonoBehaviour
{
    [SerializeField] float maxIntensity = 1f;
    [SerializeField] float minIntensity = 0.2f;
    [SerializeField] float durationSeconds = 0.5f;
    [SerializeField] float repeatDelay = 0.1f;
    [SerializeField] int twinklesAmount = int.MaxValue;

    [SerializeField] float maxIntensityDeviation = 0.5f;
    [SerializeField] float durationDeviation = 0.3f;
    [SerializeField] float repeatDelayDeviation = 0.1f;
    [SerializeField] int twinklesAmountDeviation = 0;

    [SerializeField] bool startAutomatically = true;

    private Light2D light;

    float twinkleTimer = 0;
    float delayTimer = 0;

    private int twinklesLeft = 0;
    private float thisTwinkleDuration;
    private float thisTwinkleMaxIntensity;
    private float thisTwinkleDelay;

    void Start()
    {
        light = GetComponent<Light2D>();
        light.intensity = 0;
        if (startAutomatically)
            StartTwinkling();
    }

    void Update()
    {
        if (twinklesLeft > 0)
        {
            if (delayTimer <= 0)
            {
                twinkleTimer += Time.deltaTime;
                light.intensity = sine01(twinkleTimer, thisTwinkleDuration);

                if (twinkleTimer > thisTwinkleDuration)
                {
                    twinkleTimer -= thisTwinkleDuration;
                    twinklesLeft--;
                    delayTimer = thisTwinkleDelay;
                    prepareNextTwinkle();
                }
            }
            else
            {
                light.intensity = 0;
                delayTimer -= Time.deltaTime;
            }
        }
    }

    float sine01(float value, float valuefor1)
    {
        return Mathf.Abs(Mathf.Sin(value / valuefor1 * Mathf.PI));
    }

    public void StartTwinkling()
    {
        twinkleTimer = 0;
        twinklesLeft = PlusMinus(twinklesAmount, twinklesAmountDeviation);
        prepareNextTwinkle();
    }


    public float PlusMinus(float value, float deviation)
    {
        return Random.Range(value - deviation, value + deviation);
    }
    public int PlusMinus(int value, int deviation)
    {
        return Random.Range(value - deviation, value + deviation);
    }

    public void prepareNextTwinkle()
    {
        thisTwinkleDuration = PlusMinus(durationSeconds, durationDeviation);
        thisTwinkleMaxIntensity = PlusMinus(maxIntensity, maxIntensityDeviation);
        delayTimer = 0;
    }
}
