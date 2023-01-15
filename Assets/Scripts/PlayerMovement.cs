using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fallSpeedMultiplier;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float waterSpeedMultiplier;
    private float maxWaterMoveSpeed;
    [SerializeField] private float waterGravityScale;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D collider;

    private const float IsGroundedRayLength = 0.6f;

    private Animator animator;


    private bool isWalking = false;
    private bool isFacingRight = true;
    private bool isClimbing = false;

    private float coyoteTime = 0.2f; // Ile sekund za pozno mozna wykonac skok
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f; // Ile sekund za wczesnie mozna wykonac skok
    private float jumpBufferCounter;
    private float gravityScale;
    private float horizontal;
    private float vertical;

    public bool inWater = false;

    public bool canMove = true;
    AudioSource audioSrc;

   



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        gravityScale = rigidBody.gravityScale;
        maxWaterMoveSpeed = moveSpeed * waterSpeedMultiplier;
       audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state != GameManager.GameState.Game)
            return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Move();
        Jump();
        Fall();

        animator.SetBool("isGrounded", IsGrounded());
        animator.SetBool("isWalking", isWalking);

        
    }

    void Move()
    {
      
        if (horizontal != 0f)
        {
            //Zamienilem metode flip, na rotowanie obiektu o 180st w Y, zeby moc dodac obiekt na rece, z ktorej wylatuja pociski,
            //bo inaczej obracal sie sam sprite, a reka caly czas wskazywala na jeden kierunek
            if (horizontal < 0 && isFacingRight)
                Flip();
            else if (horizontal > 0 && isFacingRight == false)
                Flip();
        }
        if (inWater)
        {
            audioSrc.Stop();
            rigidBody.velocity = new Vector2(horizontal * maxWaterMoveSpeed, rigidBody.velocity.y);
           
        }
        else
        {
            if(audioSrc.isPlaying == false && horizontal!=0 && rigidBody.velocity.y ==0)
                 audioSrc.Play();
            if (horizontal == 0 || rigidBody.velocity.y !=0)
                audioSrc.Stop();
              
            rigidBody.velocity = new Vector2(horizontal * moveSpeed, rigidBody.velocity.y);
            if (isClimbing)
            {
                
                rigidBody.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
            }
            else
                isClimbing = false;


            animator.SetBool("isClimbing", isClimbing);

        }


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
        {
           
            jumpBufferCounter = jumpBufferTime;
        }
        else
            jumpBufferCounter -= Time.deltaTime;

        //Skoki sa buforowane, jezeli wcisniemy skok kilka klatek zbyt wczesnie i tak zostanie on zarejestrowany
        if (inWater)
        {
            if (Input.GetButton("Jump"))
            {
                rigidBody.velocity += Vector2.up * jumpSpeed * Time.deltaTime*2;
            }
        }
        else
        {
            if (jumpBufferCounter > 0f&& (coyoteTimeCounter > 0f||isClimbing))
            {
             
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
                jumpBufferCounter = 0f;
                isClimbing = false;

            }
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

    void Flip()
    {
        transform.Rotate(0, 180f, 0);
        isFacingRight = !isFacingRight;
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, IsGroundedRayLength, jumpableGround.value);
    }

    public void Climb(bool val)
    {
        isClimbing = val;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = true; // ustawiamy flagê, która mo¿e siê przydaæ gdzie indziej
            rigidBody.gravityScale = gravityScale * waterGravityScale; // wolniejsze opadanie w wodzie
        }
        else if (other.CompareTag("MovingPlatform"))
        {
            Debug.Log("On a platform!");
            transform.SetParent(other.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            rigidBody.velocity = new Vector2( // W wodzie szybkosc ruchu jest ograniczona
                Mathf.Clamp(rigidBody.velocity.x, -maxWaterMoveSpeed, maxWaterMoveSpeed),
                Mathf.Clamp(rigidBody.velocity.y, -maxWaterMoveSpeed, maxWaterMoveSpeed)
                );
        }
        else if (other.CompareTag("ClimbingWall"))
        {
            if (GetComponent<PlayerItems>().hasAxes)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    Debug.Log("Pressed W");
                    Climb(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            rigidBody.gravityScale = gravityScale; // przywracamy wartoœci z przed wejœcia do wody
            inWater = false;

            if (Input.GetButton("Jump")) // mo¿na wyskoczyæ z wody
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }
        }
        else if (collision.CompareTag("MovingPlatform"))
        {
            if (transform.parent = collision.transform)
                transform.SetParent(null);
        }
        else if (collision.CompareTag("ClimbingWall"))
        {
            Climb(false);
        }
    }

    public void BlockMove(bool val)
    {
        if(val)
            moveSpeed =0;
        else 
            moveSpeed = 8;
    }
}
