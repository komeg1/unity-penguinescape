using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowInstruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshPro>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<TextMeshPro>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<TextMeshPro>().enabled = false;
        }
    }
}
