using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStats : MonoBehaviour
{
    [SerializeField]
    float health;
    int str;
    int intelligence;
    int dexterity;
    float damageCoolDown;
    [SerializeField]
    float maxDamageTimer;
    float baseHeal;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        baseHeal = 1;
        maxDamageTimer = 0.1f;
        baseHeal = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetHealth()
    {
        return health;
    }
    public void DoDamage(float damage)
    {
        health -= damage;
    }
    public void healDamage(float amountHeal= 0)
    {
        if(amountHeal != 0)
            health += amountHeal;
        else
        {
            health += baseHeal;
        }
    }
}
