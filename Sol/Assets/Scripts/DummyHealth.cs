using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHealth : MonoBehaviour, IEnemy
{
    public int ID { get; set; }
    public int Experience { get; set; }

    public int maxHealth;

    //maybe want to add this to IEnemy?
    public DamageNumber dn;

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

        //The Damage number part////
        if (dn != null) {
            DamageNumber newDN = Instantiate(dn, transform.position, Quaternion.identity);
            newDN.SetValues(amount, currentHealth / (float)maxHealth, false); //need to find a way to tell this we're criting or not
        }
        ////

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
