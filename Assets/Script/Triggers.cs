using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public HealthManagerScript health;
    public PlayerMovement movement;
    [SerializeField] Rigidbody2D coyote;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("badCollectibles")) {
            Destroy(collision.gameObject);
            health.TakeDamage();
        }
        else if (collision.gameObject.CompareTag("goodCollectibles"))
        {
            Destroy(collision.gameObject);
            health.Heal();
        }
        else if (collision.gameObject.CompareTag("fire")) {
            health.TakeDamage();
            movement.maxMoveSpeed = 0.5f;
            //todo: half second before it takes damage again
        }
        else if (collision.gameObject.CompareTag("trap")) {
            health.TakeDamage();
            
            //slow down for 2 seconds
            var origMaxMoveSpeed = movement.maxMoveSpeed;
            var origJumpForce = movement.jumpForce;
            movement.maxMoveSpeed *= 0.4f;
            movement.jumpForce *= 0.4f;

            yield return new WaitForSeconds(2);
            movement.maxMoveSpeed = origMaxMoveSpeed;
            movement.jumpForce = origJumpForce;
        }
    }
}
