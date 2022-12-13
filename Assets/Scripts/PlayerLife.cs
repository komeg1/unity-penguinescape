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

    private List<Image> healthIconList = new();

    private Animator animator = null;
    private PlayerMovement movementScript;
    private SpriteRenderer renderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        movementScript = GetComponent<PlayerMovement>();
        renderer = GetComponent<SpriteRenderer>();

        createLifeBar();
        for (int i = 0; i < health; i++)
            healthIconList[i].enabled = true;
    }


    public void Hurt()
    {
        Debug.Log("Hurt");
        DecreaseHealth();
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        rb.isKinematic = true;
        yield return new WaitForSeconds(1);

        GameManager.instance.Lose();
        renderer.forceRenderingOff = true;
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
                GameManager.instance.IncreaseKilledEnemies();
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
        else if (collision.collider.CompareTag("KillableDamageSource"))
        {
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
            health = 0;
            Hurt();
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
}
