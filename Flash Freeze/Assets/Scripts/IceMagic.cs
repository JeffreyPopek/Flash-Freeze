using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMagic : MonoBehaviour
{
    [SerializeField] float speed = 20;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        //Magic can't hit ice spike box collider
        Physics2D.IgnoreLayerCollision(8, 9);

        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.HitEnemy();
        }

        Destroy(gameObject);
    }
}
