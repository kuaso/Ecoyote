using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggersScript : MonoBehaviour
{
    public HealthManagerScript healthManager;
    private PlayerMovement _movement;
    private Rigidbody2D _coyoteRb;

    private float _origMaxMoveSpeed;
    private bool _isTrapped;

    void Start()
    {
        _coyoteRb = GetComponent<Rigidbody2D>();
        _movement = GetComponent<PlayerMovement>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (healthManager.Health != 0)
        {
            if (collision.gameObject.CompareTag("badCollectibles"))
            {
                Destroy(collision.gameObject);
                yield return healthManager.TakeDamage();
            }
            else if (collision.gameObject.CompareTag("goodCollectibles"))
            {
                Destroy(collision.gameObject);
                healthManager.Heal();
            }
            else if (collision.gameObject.CompareTag("trap"))
            {
                yield return healthManager.TakeDamage();

                // Only update original max speed and jump if the values have not been changed by the trap
                if (!_isTrapped)
                {
                    _isTrapped = true;
                    _origMaxMoveSpeed = _movement.maxMoveSpeed;
                }

                // Slow down for the entirety of the time that the player is in the trap
                _movement.maxMoveSpeed = _origMaxMoveSpeed * 0.2f;
            }
        }
    }

    private IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("fire"))
        {
            yield return healthManager.TakeDamage();
            // todo: half second before it takes damage again
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (healthManager.Health != 0)
        {
            if (collision.gameObject.CompareTag("trap"))
            {
                // Make use wait for 2 seconds after exiting the trap before restoring normal speed
                yield return new WaitForSeconds(2);
                _movement.maxMoveSpeed = _origMaxMoveSpeed;
                _isTrapped = false;
            }
        }
    }
}