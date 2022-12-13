using System.Collections;
using UnityEngine;

public class SpriteDeathScript : MonoBehaviour
{
    [SerializeField] private string animatorDeathBoolName;
    [SerializeField] private float delay;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    public void Death()//DIE
    {
        GetComponent<Collider2D>().enabled = false;
        animator.SetBool(animatorDeathBoolName, true);
        StartCoroutine(DeathCoroutine());
    }
}
