using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSnow( float maxSnow )
    {
        slider.maxValue= maxSnow;
    }

    public void SetSnow(float snowAmount) 
    {
        slider.value = snowAmount;
    }
}
