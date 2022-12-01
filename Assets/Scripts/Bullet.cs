using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private Rigidbody2D bullet;
   
    // Start is called before the first frame update
    void Start()
    {
        bullet.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
