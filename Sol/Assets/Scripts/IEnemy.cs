using System.Collections;
using UnityEngine;

public interface IEnemy
{
    //Stats given to all enemies

    int ID { get; set; }
    int Experience { get; set; }
    void TakeDamage(int amount);
    void Die();
}
