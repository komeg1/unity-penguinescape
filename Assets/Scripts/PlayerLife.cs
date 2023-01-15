using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{

    [SerializeField] private Texture2D healthIcon;
    [SerializeField] private int healthIconPixelsPerUnit;
    [SerializeField] private GameObject healthBar;
    [SerializeField] public int health = 1;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float hurtKnockbackSpeed = 5f;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip damageSound;


    [SerializeField] float immunityTime = 1f;
    [SerializeField] int immunityEffectFlashes = 4;
    float timeSinceLastHurt;

    private List<Image> healthIconList = new();

    private AudioSource audioSource;
    private Animator animator = null;
    private PlayerMovement movementScript;
    private SpriteRenderer renderer;

    void Start()
    {
        timeSinceLastHurt = immunityTime;
        animator = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMovement>();
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        createLifeBar();
        for (int i = 0; i < health; i++)
            healthIconList[i].enabled = true;
    }

    private void Update()
    {
        timeSinceLastHurt += Time.deltaTime;
        if (timeSinceLastHurt <= immunityTime)
        {
            float value = (Mathf.Cos((timeSinceLastHurt - immunityTime)/immunityTime * immunityEffectFlashes * 2 * Mathf.PI) + 1) / 2;
            renderer.color = new Color(1f, value, value);
        }

    }


    public void Hurt()
    {
        Debug.Log("Hurt");
        if(timeSinceLastHurt >= immunityTime)
        {
            timeSinceLastHurt = 0;
            DecreaseHealth();
            audioSource.PlayOneShot(damageSound, AudioListener.volume);
        }

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
        audioSource.PlayOneShot(deathSound, AudioListener.volume);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        yield return new WaitForSeconds(1);

        GameManager.instance.Lose();
        renderer.forceRenderingOff = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("DamageSource"))
        {
            if(health>0)
                Hurt();
        }
        else if (collision.CompareTag("KillableDamageSource"))
        {
            Debug.Log("Killable enemy contact");
            if (transform.position.y > collision.transform.position.y)
            {
                collision.gameObject.GetComponent<SpriteDeathScript>().Death();
                GameManager.instance.IncreaseKilledEnemies();
            }
            else
            {
                Hurt();
            }
        }
        else if (collision.CompareTag("InstaDeath"))
        {
            Death();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("DamageSource"))
        {
            if (health > 0)
                Hurt();
        }
        else if (collision.collider.CompareTag("KillableDamageSource"))
        {
            Debug.Log("Killable enemy contact");
            if (transform.position.y > collision.transform.position.y)
            {
                collision.gameObject.GetComponent<SpriteDeathScript>().Death();
                GameManager.instance.IncreaseKilledEnemies();
            }
            else
            {
                Hurt();
            }
        }
        else if (collision.collider.CompareTag("InstaDeath"))
        {
            Death();
        }
    }

    void createLifeBar()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject newHealthIcon = new GameObject();
            RectTransform healthIconTransform = newHealthIcon.AddComponent<RectTransform>();
            healthIconTransform.sizeDelta = new Vector2(50, 50);

            Image heartIconImage = newHealthIcon.AddComponent<Image>();
            healthIconList.Add(heartIconImage);

            Sprite healthIconSprite = Sprite.Create(healthIcon, new Rect(0f, 0f, healthIcon.width, healthIcon.height), new Vector2(0f, 0f), healthIconPixelsPerUnit);
            heartIconImage.sprite = healthIconSprite;
            heartIconImage.enabled = false;

            newHealthIcon.transform.SetParent(healthBar.transform);
            newHealthIcon.transform.localPosition = new Vector2(healthIconTransform.sizeDelta.x / 2 + i * healthIconTransform.sizeDelta.x, 0f);
        }

    }

    public void IncreaseHealth()
    {
        if (health >= 0 && health < healthIconList.Count)
            healthIconList[health].enabled = true;
        health++;
    }
    public void DecreaseHealth()
    {
        health--;
        if (health >= 0 && health < healthIconList.Count)
            healthIconList[health].enabled = false;
    }

    public bool MaxHealth()
    {
        return health == maxHealth;
    }
    public int GetHealth()
    {
        return health;
    }
}
