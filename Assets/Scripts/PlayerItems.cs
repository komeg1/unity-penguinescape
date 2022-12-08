using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public bool hasAxes = false;
   [SerializeField] private float snowToAddAmount = 0.5f;

    //snieg
    public float pickedSnowAmount = 0.0f;

    public void UpdateSnowAmount()
    {
        if(pickedSnowAmount < 100.0f)
        pickedSnowAmount += snowToAddAmount;
    }
  
}
