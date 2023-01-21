using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButtonScript : MonoBehaviour
{

    GateOpenerScript gateOpener;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        gateOpener= GetComponent<GateOpenerScript>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Snowball"))
        {
            animator.SetBool("Pressed", true);
            collision.gameObject.SetActive(false);
            gateOpener.Open();
        }
    }
}
