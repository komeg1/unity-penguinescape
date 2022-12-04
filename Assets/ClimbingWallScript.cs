using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement movement;
    public PlayerItems player;
   

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.hasAxes)
        {
            movement.Climb(true);
            Debug.Log("Can climb" + player.hasAxes);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && player.hasAxes)
        {
            movement.Climb(false);
            Debug.Log("Can climb");
        }
    }
}
