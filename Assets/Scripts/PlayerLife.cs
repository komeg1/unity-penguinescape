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
        movementScript.canMove = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
        deathText.gameObject.SetActive(true);
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
