using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{

    public bool hasAxes = false;
  
    

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

    
   
}
