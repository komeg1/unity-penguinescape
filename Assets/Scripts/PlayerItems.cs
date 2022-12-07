using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{

    public bool hasAxes = false;

    //snieg
    public float pickedSnowAmount = 0.0f;
   [SerializeField] private float snowToAddAmount = 0.5f;
  
    

    // Update is called once per frame
 


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IceAxes"))
        {
            
            hasAxes = true;
            Debug.Log("Picked up axes");
            other.GetComponent<PickUpScript>().disappear();
          
        }
        
    }

    public void UpdateSnowAmount()
    {
        if(pickedSnowAmount < 100.0f)
        pickedSnowAmount += snowToAddAmount;
    }

    
   
}
