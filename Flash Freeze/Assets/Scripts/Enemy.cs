using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.1f;
    [SerializeField] Transform[] movePoints;
    [SerializeField] BoxCollider2D bc2D;

    bool facingRight;

    int _pointIndex = 0;
    private Transform _currentPoint;


    //Freeze enemy
    bool isFrozen = false;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite enemy;
    [SerializeField] Sprite frozenEnemy;

    void Awake()
    {
        _currentPoint = movePoints[_pointIndex];
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentPoint.position, moveSpeed);

        if (Vector2.Distance(transform.position, _currentPoint.position) < 0.01f)
        {
            _pointIndex++;
            _pointIndex %= movePoints.Length;
            _currentPoint = movePoints[_pointIndex];
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void HitEnemy()
    {
        if (isFrozen)
        {
            isFrozen = false;
            moveSpeed = 0.1f;
            gameObject.tag = "Hazard";
            gameObject.layer = 10;

            GetComponent<BoxCollider2D>().size = new Vector2(15f, 12f);
        }
        else
        {
            isFrozen = true;
            moveSpeed = 0;
            gameObject.tag = "Ground";
            gameObject.layer = 6;

            GetComponent<BoxCollider2D>().size = new Vector2(17f, 13f);

        }
        ChangeSprite();
    }

    void ChangeSprite()
    {
        if(isFrozen)
        {
            spriteRenderer.sprite = frozenEnemy;
        }
        else
        {
            spriteRenderer.sprite = enemy;
        }
    }
}
