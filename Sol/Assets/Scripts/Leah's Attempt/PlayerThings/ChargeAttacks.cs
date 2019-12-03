﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttacks : MonoBehaviour
{
    public bool[] activeBools;

    [Header("Reset Stuff")]
    public int activeSkill = -1;
    public bool charged = false, isCharging = false;
    public bool isAttacking = false;
    public float chargeTime = 0F, timeToCharge, colorTesting;
    public GameObject sonicPref;

    // while charging: should not be able to do a normal attack, cannot change direction(?)/cannot move
    // while attacking: cannot do a normal attack, depending if attack has an animation could be able to move again

    void Start()
    {
        activeBools = new bool[3] { false, false, false };
    }

    void Update()
    {
        if (activeSkill != -1 && Inventory.inventory.equipSlots[1].itemID != 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                isCharging = true;
                chargeTime += Time.deltaTime;
                if (chargeTime >= timeToCharge)
                {
                    charged = true;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
                }
                else
                {
                    colorTesting = (timeToCharge - chargeTime) * 255F;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, (byte)colorTesting, 255);
                }
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                isCharging = false;
                //reset values
                if (charged) DoAttack(activeSkill);
                chargeTime = colorTesting = 0F;
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (chargeTime != 0) chargeTime = 0;
            if (colorTesting != 0) colorTesting = 0;
        }
    }

    void DoAttack(int aSkill)
    {
        if (Inventory.inventory.equipSlots[1].itemID != 0)
        {
            isAttacking = true;
            if (aSkill == 0) StartCoroutine(SonicBlastAttack());
            else if (aSkill == 1) BoomerangThrowAttack();
            else if (aSkill == 2) numThree();
            else
            {
                Debug.Log("No charge attacks active.");
                charged = isAttacking = false;
            }
        }
        else
        {
            Debug.Log("No weapon equipped.");
            charged = isAttacking = false;
        }
    }

    IEnumerator SonicBlastAttack()
    {
        Debug.Log("BOOOM sonic blast attack!");

        // mimic dodge movement
        //instansiate blast object
        //give blast attack values

        WaitUntil wait = new WaitUntil(() => isAttacking = false);
        yield return wait;

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