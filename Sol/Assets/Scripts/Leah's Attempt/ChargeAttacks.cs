using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttacks : MonoBehaviour
{
    public bool[] activeBools;

    [Header("Reset Stuff")]
    public int activeSkill = -1;
    public bool charged = false;
    public bool isAttacking = false;
    public float chargeTime = 0F, timeToCharge;

    // while charging: should not be able to do a normal attack, cannot change direction(?)/cannot move
    // while attacking: cannot do a normal attack, depending if attack has an animation could be able to move again

    void Start()
    {
        activeBools = new bool[3] { false, false, false };
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            chargeTime += Time.deltaTime;
            if (chargeTime >= timeToCharge) charged = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            //reset values
            if (charged) DoAttack(activeSkill);
            chargeTime = 0F;
        }
    }

    void DoAttack(int aSkill)
    {
        if (aSkill == 0) SonicBlastAttack();
        else if (aSkill == 1) BoomerangThrowAttack();
        else if (aSkill == 2) numThree();
        else Debug.Log("No charge attacks active.");
        charged = false;
    }

    void SonicBlastAttack()
    {
        Debug.Log("BOOOM sonic blast attack!");
    }

    void BoomerangThrowAttack()
    {
        Debug.Log("WHOOSH boooomerang throwwww");
    }

    void numThree()
    {
        Debug.Log("idk what this is still");
    }
}
