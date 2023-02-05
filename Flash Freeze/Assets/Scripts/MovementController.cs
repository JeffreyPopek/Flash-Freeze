using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask groundLayer;

    //x movement
    [SerializeField] float xSpeed = 1;
    float horizontalValue;

    //jumping
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float buttonTime = 0.5f;
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float cancelRate = 100;
    float jumpTime;
    bool jumping;
    bool jumpCancelled;

    [SerializeField] int maxJumps;
    int jumpCount = 0;


    //sound
    //public GameObject audioManager;
    bool isMoving = false;

    //start game facing right
    bool facingRight = true;

    //hazards
    string sceneName;

    void Awake()
    {
        //reload into current scene
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        jumpCount = maxJumps;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Movement Script Working");

        //Player can't run into magic
        Physics2D.IgnoreLayerCollision(7, 8);        
    }

    private void Start()
    {

    }

    void Update()
    {
        //left: -1, nothing: 0, right: 1
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if(rb.velocity.x < 0.1f)
        {
            isMoving = true;
        }

        
        //sound
        if(isMoving)
        {
           
        }
        else
        {

        }


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            if(jumpCount > 0)
            {
                Jump();

                //sound
                
            } 
        }

        //jumping at different heights based on button press
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
    }

    void FixedUpdate()
    {
        Move(horizontalValue);

        if (jumpCancelled && jumping && rb.velocity.y > 0)
        {
            rb.AddForce(Vector2.down * cancelRate);
        }
    }

    private bool IsGrounded()
    {
        float heightTest = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, heightTest, groundLayer);

        Color rayColor;

        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red; 
        }
        
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + heightTest), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + heightTest), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + heightTest), Vector2.right * (boxCollider2D.bounds.extents.x), rayColor);


        Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;
    }

    void Flip()
    {
        facingRight = !facingRight;

        //rotate player instead of changing local scale so fire point is also flipped
        transform.Rotate(0f, 180f, 0f);
    }

    void Move(float direction)
    {
        float xVal = direction * (xSpeed * 100) * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        //flip player
        if(horizontalValue < 0 && facingRight)
        {
            Flip();
        }
        else if(horizontalValue > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * rb.gravityScale));
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumping = true;
        jumpCancelled = false;
        jumpTime = 0;
        jumpCount -= 1;

        //sound
        FindObjectOfType<AudioManager>().Play("PlayerJump"); ;
    }

    void Die()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = maxJumps;
            FindObjectOfType<AudioManager>().Play("PlayerLand");
        }

        if (collision.gameObject.tag == "Hazard")
        {
            Die();
        }
    }
}
