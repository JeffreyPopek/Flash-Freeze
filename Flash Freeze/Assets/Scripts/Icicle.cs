using Unity.VisualScripting;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] private float newGravityScale = 4;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
            //rb.gravityScale = newGravityScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);

            //sound
            FindObjectOfType<AudioManager>().Play("IceBreak");
        }
    }
}
