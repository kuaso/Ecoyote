using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public HealthManagerScript health;
    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    public Rigidbody2D coyoteRb;
    [SerializeField] float speed;
    public SpriteRenderer enemySprite;
    private Transform currentPoint;

    private void Start()
    {
        coyoteRb = GetComponent<Rigidbody2D>();
        currentPoint = pointA.transform;

    }

    private void Update()
    {
        Vector2 point = transform.position - currentPoint.position;
        if (currentPoint == pointA.transform) {
            coyoteRb.velocity = new Vector2(-speed, 0);
        }
        else {
            coyoteRb.velocity = new Vector2(speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform) {
            currentPoint = pointB.transform;
            enemySprite.flipX = true;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
            enemySprite.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("coyote")) {
            health.TakeDamage();
        }
    }

}
