using UnityEngine;

public class OpossumAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float groundCheckOffsetX = 1f;
    [SerializeField] private float groundCheckRayLength = 1f;
    [SerializeField] private LayerMask jumpableGround;
    private float moveDirection = 1f;
    [SerializeField] private bool initialFlipX = false;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (initialFlipX)
            spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rayStart = new Vector2(transform.position.x + moveDirection * groundCheckOffsetX, transform.position.y);
        Debug.DrawLine(rayStart, rayStart + Vector2.down * groundCheckRayLength, Color.red, 0);
        bool groundAhead = Physics2D.Raycast(rayStart, Vector2.down, groundCheckRayLength, jumpableGround.value);
        bool wallAhead = Physics2D.Raycast(transform.position, new Vector2(moveDirection, 0), groundCheckRayLength, jumpableGround.value);
        if (!groundAhead || wallAhead)
        {
            moveDirection *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        rigidbody.velocity = new Vector2(moveSpeed * moveDirection, rigidbody.velocity.y);

    }
}
