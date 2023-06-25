using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    private int _health = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    public int Health => _health;

    public void Heal(int healing = 1)
    {
        if (healing < 0) throw new System.Exception("Healing cannot be negative");
        if (_health >= 3) _health = 3; // Cap at 3
        else _health += healing;
        hearts[_health - 1].sprite = fullHeart;
    }
    
    public void TakeDamage(int damage = 1)
    {
        if (damage < 0) throw new System.Exception("Damage cannot be negative");
        _health -= damage;
        hearts[_health].sprite = emptyHeart;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // TODO game over screen
    }

}
