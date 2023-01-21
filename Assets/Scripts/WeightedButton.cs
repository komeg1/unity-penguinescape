using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedButton : MonoBehaviour
{
    GateOpenerScript gateOpener;
    Animator animator;

    private void Start()
    {
        gateOpener = GetComponent<GateOpenerScript>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Snowball"))
        {
            gateOpener.Open();
            animator.SetBool("Pressed", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Snowball"))
        {
            gateOpener.Close();
            animator.SetBool("Pressed", false);
        }
    }

}
