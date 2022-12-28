using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlockBuilder : MonoBehaviour
{
    public GameObject spherePrefab;

    // A reference to the current sphere
    private GameObject sphere;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // Create the sphere at the player's position if it doesn't already exist
            if (sphere == null)
            {
                Vector3 playerPos = transform.position;
                playerPos.x += 0.1f;
                sphere = Instantiate(spherePrefab,playerPos, Quaternion.identity);
            }

            GetComponent<PlayerMovement>().BlockMove(true);
            // Scale up the sphere over time
            sphere.transform.localScale += Vector3.one * Time.deltaTime *0.5f;
        }
        else
        {
            GetComponent<PlayerMovement>().BlockMove(false);
            // Destroy the sphere if the right mouse button is not being held down
            sphere = null;
        }
    }
}

