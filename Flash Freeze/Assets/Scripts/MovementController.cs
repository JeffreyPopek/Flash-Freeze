using System.Linq.Expressions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
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

    //start game facing right
    bool facingRight = true;

    //hazards
    string sceneName;



    void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        jumpCount = maxJumps;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Movement Script Working");
    }

    void Update()
    {
        //left: -1, nothing: 0, right: 1
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpCount > 0)
            {
                Jump();
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
        }

        if (collision.gameObject.tag == "Hazard")
        {
            Die();
        }
    }

}
