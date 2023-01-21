using System.Collections;
using UnityEngine;

public class SpriteDeathScript : MonoBehaviour
{
    [SerializeField] private string animatorDeathBoolName;
    [SerializeField] private float delay;
    [SerializeField] AudioClip deathSound;
    private Animator animator;
    private const float volumeMultipler = 0.5f;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    public void Death()//DIE
    {
        GetComponent<Collider2D>().enabled = false;
        audioSource.PlayOneShot(deathSound, AudioListener.volume * volumeMultipler);
        animator.SetBool(animatorDeathBoolName, true);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        StartCoroutine(DeathCoroutine());
    }
}
