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

    private const float IsGroundedRayLength = 1.5f;

    private int score = 0;
    private Animator animator;
    private bool isWalking = false;

    private float coyoteTime = 0.2f; // Ile sekund za pozno mozna wykonac skok
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f; // Ile sekund za wczesnie mozna wykonac skok
    private float jumpBufferCounter;

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
        // Zamiast sprawdzac czy postac jest uziemiona bedziemy sprawdzac jak dawno temu byla uziemiona
        // Dzieki temu mozna skoczyc z lekkim opoznieniem po opuszczeniu stabilnego gruntu
        if (IsGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        //Skoki sa buforowane, jezeli wcisniemy skok kilka klatek zbyt wczesnie i tak zostanie on zarejestrowany
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            jumpBufferCounter = 0f;
        }
    }

    private void Fall()
    {
        if (Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
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
        return Physics2D.Raycast(transform.position, Vector2.down, IsGroundedRayLength, jumpableGround.value);
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
