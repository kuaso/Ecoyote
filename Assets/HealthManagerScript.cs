using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int health;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public void Heal(int healing = 1)
    {
        if (healing < 0) throw new System.Exception("Healing cannot be negative");
        if (health >= 3) health = 3; // Cap at 3
        else health += healing;
    }
    
    public void TakeDamage(int damage = 1)
    {
        if (damage < 0) throw new System.Exception("Damage cannot be negative");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // TODO game over screen
    }

}
