using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{

    [SerializeField] private TMPro.TextMeshProUGUI deathText;
    [SerializeField] private GameObject lifeCounterTextObject;
    [SerializeField] public int health = 1;
    public TMPro.TextMeshProUGUI lifeCounterText;

    private Animator animator = null;
    private PlayerMovement movementScript;
    private SpriteRenderer renderer;
    public string healthCounterPrefix = "Health: ";
    void Start()
    {
        lifeCounterText = lifeCounterTextObject.GetComponent<TMPro.TextMeshProUGUI>();
        deathText.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMovement>();
        renderer = GetComponent<SpriteRenderer>();

        lifeCounterText.SetText(healthCounterPrefix + health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hurt()
    {
        Debug.Log("Hurt");
        health -= 1;
        lifeCounterText.SetText(healthCounterPrefix + health);
        if (health <= 0)
            Death();
    }

    public void Death()
    {
        animator.SetBool("died", true);
        StartCoroutine(DeathCoroutine());
    }
    IEnumerator DeathCoroutine()
    {
        movementScript.canMove = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        yield return new WaitForSeconds(1);

       
        deathText.gameObject.SetActive(true);
        StartCoroutine(ReloadScene());
        renderer.forceRenderingOff = true;
        //gameObject.SetActive(false);

    }
    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageSource"))
        {
            Hurt();
        }
        else if (collision.CompareTag("KillableDamageSource"))
        {
            Debug.Log("Killable enemy contact");
            if (transform.position.y > collision.transform.position.y)
            {
                collision.gameObject.GetComponent<SpriteDeathScript>().Death();
            }
            else
            {
                Hurt();
            }
        }
        else if (collision.CompareTag("InstaDeath"))
        {
            health = 0;
            Hurt();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DamageSource"))
        {
            Hurt();
        }
    }
}
