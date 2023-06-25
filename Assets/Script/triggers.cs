using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollector : MonoBehaviour
{
    public HealthManagerScript health;
    [SerializeField] Rigidbody2D coyote;

    private void OnTriggerEnter2D(Collider2D collision)
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
            //todo: half second before it takes damage again
        }
        else if (collision.gameObject.CompareTag("trap")) {
            health.TakeDamage();
            //todo: slow down for 2 seconds
        }
    }
}
