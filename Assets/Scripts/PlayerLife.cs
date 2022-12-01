using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI deathText;
    private Animator animator = null;
    private PlayerMovement movementScript;

    void Start()
    {
        deathText.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Death()
    {
        Debug.Log("Death anim");
        yield return new WaitForSeconds(1);
        Debug.Log("Death");
        gameObject.SetActive(false);
        deathText.gameObject.SetActive(true);
        movementScript.canMove = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageSource"))
        {
            animator.SetBool("died", true);
            StartCoroutine(Death());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DamageSource"))
        {
            animator.SetBool("died", true);
            Debug.Log("Hit!");
            StartCoroutine(Death());
        }
    }
}
