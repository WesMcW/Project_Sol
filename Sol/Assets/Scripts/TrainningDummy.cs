using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainningDummy : MonoBehaviour, IEnemy
{

    public int ID { get; set; }
    public int Experience { get; set; }
    public void TakeDamage(int damage) {
        Debug.Log("BOOM: " + damage);
    }

    public void Die() {

    }
}
