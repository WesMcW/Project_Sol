using System.Collections;
using UnityEngine;

public interface IEnemy
{
    int ID { get; set; }
    int Experience { get; set; }
    void TakeDamage(int amount);
    void Die();
}
