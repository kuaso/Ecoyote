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
    public CoyoteStateScript coyoteCoyoteStateScript;
    // Use frames instead of time because we need we care about the perception of the animation rather than the real time
    private int _framesDamageShown = 16; // Start with a value higher than max to prevent from running
    private const int FramesDamageShownMax = 15; // DON'T MAKE THIS TOO LONG AS HURT ANIMATION IS STATIC DUE TO LACK OF TIME
    private bool _overrideMaxFramesDamageShown;
    public bool hasDied;

    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip dieSound;

    // To use in other scripts, assign this script to public HealthManagerScript healthManager; in other scripts
    public void Update()
    {
        if (_overrideMaxFramesDamageShown || _framesDamageShown < FramesDamageShownMax)
        {
            coyoteCoyoteStateScript.UpdateAnimationState(CoyoteStateScript.AnimationState.Hurt);
            _framesDamageShown++;
        }
    }

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
        _framesDamageShown = 0;
        if (_health <= 0)
        {
            yield return Die();
        }
        sound.instance.PlaySound(damageSound);
    }

    public IEnumerator Die()
    {
        if (!hasDied)
        {
            hasDied = true;
            sound.instance.PlaySound(dieSound);
            _overrideMaxFramesDamageShown = true;
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
            
            // Restart level
            // Might cause NullReferenceExceptions, but that doesn't matter since we are changing scenes
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _health = 3;
            foreach (var heart in hearts)
            {
                heart.sprite = fullHeart;
            }
        }
    }
}