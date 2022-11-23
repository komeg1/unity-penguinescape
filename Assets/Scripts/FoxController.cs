using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float jumpForce = 6.0f;
    [SerializeField] private LayerMask jumpableGround;
    private const float rayLength = 1.2f;
    private Rigidbody2D rigidBody2d;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidBody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float axisX = Input.GetAxis("Horizontal");
        flip(axisX);
        transform.Translate(moveSpeed * Time.deltaTime * axisX, 0.0f, 0.0f, Space.World);
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.white, 0, false);

        Jump();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rigidBody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, rayLength, jumpableGround.value);
    }

    void flip(float axisx)
    {
        if (axisx < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }
}
