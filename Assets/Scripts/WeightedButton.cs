using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedButton : MonoBehaviour
{
    [SerializeField] GateOpenerScript gateOpener;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(Physics2D.Raycast(transform.position, Vector2.up, 1f))
        {
            gateOpener.Open();
            animator.SetBool("Pressed", true);
        }
        else
        {
            gateOpener.Close();
            animator.SetBool("Pressed", false);
        }
    }


}
