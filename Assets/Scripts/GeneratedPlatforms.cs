using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratedPlatforms : MonoBehaviour
{
    [SerializeField] GameObject platformPrefarb;
    [SerializeField] float speed;
    [SerializeField] float radius = 3.5f;
    [SerializeField] int platformNum = 3;
    GameObject[] platforms;
    Vector3[] positions;
    void Awake()
    {
        platforms = new GameObject[platformNum];
        positions = new Vector3[platformNum];
        for (int i = 0; i < platformNum; i++)
        {
            positions[i] = new Vector3(this.transform.position.x+radius*Mathf.Cos((float)(2*Mathf.PI*i/platformNum)), this.transform.position.y + radius*Mathf.Sin((float)(2 * Mathf.PI * i / platformNum)),0);
            platforms[i] = Instantiate(platformPrefarb, positions[i], Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < platformNum; i++)
        {
            if (platforms[i].transform.position != positions[i])
            {
                platforms[i].transform.position = Vector3.MoveTowards(platforms[i].transform.position, positions[i], speed * Time.deltaTime);
            }
            else
            {
                (platforms[i], platforms[(i + 1) % platformNum]) = (platforms[(i + 1) % platformNum], platforms[i]);
                //platforms[i].transform.position = Vector3.MoveTowards(platforms[i].transform.position, positions[i], speed * Time.deltaTime);
            }

        }
    }
}
