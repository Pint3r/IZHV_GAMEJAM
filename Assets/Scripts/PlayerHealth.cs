using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health, maxHealth;

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    void Awake()
    {
        int healthLevel = PlayerPrefs.GetInt("HealthLevel");
        health = healthLevel + 2;

        maxHealth = health + 2;
    }

    public void Heal(int amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        OnPlayerDamaged?.Invoke();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        OnPlayerDamaged?.Invoke();

        if (health <= 0)
        {
            health = 0;
            OnPlayerDeath?.Invoke();
        }
    }
}
