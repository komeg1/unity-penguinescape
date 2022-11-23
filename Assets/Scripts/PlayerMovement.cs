using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fallSpeedMultiplier;
    [SerializeField] private float maxFallSpeed;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D collider;

    private int score = 0;
    private Animator animator;
    private bool isWalking = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Move(horizontal);
        Jump();
        Fall();

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isWalking", isWalking);
    }

    void Move(float horizontal)
    {
        if (horizontal != 0f)
        {
            if ((!spriteRenderer.flipX && horizontal < 0) || (spriteRenderer.flipX && horizontal > 0))
                Flip();       
        }
        rigidBody.velocity = new Vector2(horizontal * moveSpeed, rigidBody.velocity.y);

        isWalking = (horizontal != 0);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
    }

    private void Fall()
    {
        if (rigidBody.velocity.y > 0.1f)
        {
            if (!Input.GetButton("Jump"))
                IncreaseFallSpeed();
        }
        else if (rigidBody.velocity.y < -0.1f)
            IncreaseFallSpeed();

        CapFallSpeed();
        
    }
    void CapFallSpeed()
    {
        if (rigidBody.velocity.y < -maxFallSpeed)
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, -maxFallSpeed);
    }

    private void IncreaseFallSpeed()
    {
        rigidBody.velocity += (fallSpeedMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
    }

    void Flip()
    {
        spriteRenderer.flipX =!spriteRenderer.flipX;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1, jumpableGround.value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;
            Debug.Log("Score: " + score);
            other.gameObject.SetActive(false);
        }
    }
}
