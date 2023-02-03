using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;

    private Rigidbody2D rb;
    [SerializeField] private float force;


    private void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        rb = GetComponent<Rigidbody2D>();

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePos - transform.position;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        StartCoroutine(spellDestruct());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.HitEnemy();
        }

        Destroy(gameObject);
    }

    IEnumerator spellDestruct()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
