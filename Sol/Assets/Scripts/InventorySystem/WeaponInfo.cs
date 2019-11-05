using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : ItemInfo
{

    public int damage;
    [Header("Additional % attack speed (100% is twice as fast)")]
    [Header(".5 = 50%")]
    public float attackSpeed = 0; //Additional attack speed

    [Header(".5 = 50%")]
    public float critChance = 0;
    public float critMultiplier = 0;

    public float blockChance = 0;
    public float stunChance = 0;


}
