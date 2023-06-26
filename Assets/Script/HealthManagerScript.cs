using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthManagerScript : MonoBehaviour
{
    public int maxHealth = 3; // This really shouldn't be changed because the ui only has 3 hearts
    private int _health = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Rigidbody2D coyoteRb;
    public GameObject coyote;

    public int Health => _health;
    public bool hasDied;

    // To use in other scripts, assign this script to public HealthManagerScript healthManager; in other scripts

    public void Heal(int healing = 1)
    {
        if (healing < 0) throw new System.Exception("Healing cannot be negative");
        if (_health >= maxHealth) _health = maxHealth;
        else _health += healing;
        hearts[_health - 1].sprite = fullHeart;
    }

    public IEnumerator TakeDamage(int damage = 1)
    {
        if (damage < 0) throw new System.Exception("Damage cannot be negative");
        if (_health > 0) _health -= damage;
        hearts[_health].sprite = emptyHeart;
        if (_health <= 0)
        {
            yield return Die();
        }
    }

    public IEnumerator Die()
    {
        if (!hasDied)
        {
            hasDied = true;
            foreach (var heart in hearts)
            {
                heart.sprite = emptyHeart;
            }
            coyoteRb.transform.Rotate(180, 0, 0);
            coyoteRb.velocity = new Vector2(0, coyoteRb.gravityScale * 10f);

            while (coyoteRb.velocity.y != 0)
            {
                yield return null;
            }

            // Might cause NullReferenceExceptions, but that doesn't matter since we are changing scenes
            coyote.SetActive(false);

            // Restart level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _health = 3;
            foreach (var heart in hearts)
            {
                heart.sprite = fullHeart;
            }
            coyote.SetActive(true);
        }
    }
}