using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private int newGravityScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {  
            rb.gravityScale = newGravityScale;
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
                 
        }
    }
}
