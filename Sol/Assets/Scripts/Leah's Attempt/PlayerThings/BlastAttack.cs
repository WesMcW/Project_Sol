using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastAttack : MonoBehaviour
{
    // damage pref for the blast attack
    // not sure how this will scale in respect to player yet, but should hit all enemies in radius; potentially multiple times

    public Collider2D solidBox;
    float damage, critChance, critMulti;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setValues(GameObject player, float[] values)
    {
        //Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), solidBox);
        damage = values[0];
        critChance = values[1];
        critMulti = values[2];
    }

    void goAway()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit " + collision.gameObject.name);
        // do damage, set enemy take damage cooldown
        IEnemy enemy = collision.gameObject.GetComponent<IEnemy>();
        if (enemy != null)
        {
            Debug.Log("enemy hit");
            if (Random.Range(0f, 1f) < critChance)
            {
                enemy.TakeDamage((int)(damage * critMulti));
            }
            else
            {
                enemy.TakeDamage(Mathf.RoundToInt(damage));
            }
            // start enemy cooldown
        }
    }
}
