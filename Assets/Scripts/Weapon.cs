using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] private Transform hand;
    [SerializeField] private GameObject bulletPrefab;

    private int ammoAmount = 10;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(ammoAmount> 0)
                Shoot();

        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, hand.position, hand.rotation);
        ammoAmount--;
    }
}
