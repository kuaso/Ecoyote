using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollector : MonoBehaviour
{
    public HealthManagerScript health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("badCollectibles")) {
            Destroy(collision.gameObject);
            health.TakeDamage();
        }
        //I'll need wildfire/traps laterr
        else if (collision.gameObject.CompareTag("goodCollectibles"))
        {
            Destroy(collision.gameObject);
            health.Heal();
        }
    }
}
