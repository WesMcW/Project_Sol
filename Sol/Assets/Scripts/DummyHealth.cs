using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : MonoBehaviour, IEnemy
{
    public int ID { get; set; }
    public int Experience { get; set; }

    public int maxHealth;

    [SerializeField]
    private int currentHealth;

    void Start()
    {
        ID = 1;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        CombatEvent.EnemyDied(this);
        Destroy(gameObject);
    }
}
