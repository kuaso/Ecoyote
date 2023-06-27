using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggersScript : MonoBehaviour
{
    // EXPECT NREs WHEN HEALTH IS 0

    public HealthManagerScript healthManager;
    private PlayerMovement _movement;
    [SerializeField] private AudioClip trapSound;

    private float _origMaxMoveSpeed;
    private bool _isTrapped;


    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("badCollectables"))
        {
            Destroy(collision.gameObject);
            yield return healthManager.TakeDamage();
        }
        else if (collision.gameObject.CompareTag("goodCollectables"))
        {
            Destroy(collision.gameObject);
            healthManager.Heal();
        }
        else if (collision.gameObject.CompareTag("bullet")) {
            Destroy(collision.gameObject);
            yield return healthManager.TakeDamage();
        }

        else if (collision.gameObject.CompareTag("enemy"))
        {
            yield return healthManager.TakeDamage();
        }
        else if (collision.gameObject.CompareTag("fire"))
        {
            yield return healthManager.TakeDamage();
        }
        else if (collision.gameObject.CompareTag("trap"))
        {
            yield return healthManager.TakeDamage();
            // Only update original max speed and jump if the values have not been changed by the trap
            if (!_isTrapped)
            {
                _isTrapped = true;

                _origMaxMoveSpeed = _movement.maxMoveSpeed;
                sound.instance.PlaySound(trapSound);
                // Should be set to false on exit of trap
            }

            // Slow down for the entirety of the time that the player is in the trap
            _movement.maxMoveSpeed = _origMaxMoveSpeed * 0.2f;
        }
        else if (collision.gameObject.CompareTag("spikes"))
        {
            yield return healthManager.Die();
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trap"))
        {
            // Make player wait for 1 seconds after exiting the trap before restoring normal speed
            yield return new WaitForSeconds(1);
            _movement.maxMoveSpeed = _origMaxMoveSpeed;
            _isTrapped = false;
        }
    }
}