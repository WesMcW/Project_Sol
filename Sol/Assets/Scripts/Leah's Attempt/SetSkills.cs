﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkills : MonoBehaviour
{
    public GameObject player;
    SkillsManager sm;

    [Header("Stat Modifiers:")]
    public float rollSpdUp;
    public float critChance, critChanceUp;
    public float attackSpeed, attackSpeedUp;
    public float critDmg, critDmgUp;
    public float blockChance, blockChanceUp;
    public float stunChance, stunChanceUp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sm = GetComponent<SkillsManager>();
    }

    public void enableSkill(int x)  // less methods
    {
        //Debug.Log("x is " + x);
        switch (x)
        {
            case 0:
                {
                    if (sm.dodgeRoll[0].active) player.GetComponent<Player>().canDodge = true;
                    break;
                }
            case 1:
                {
                    if (sm.passivesRow1[0].enabled) player.GetComponent<Player>().rollSpeed = rollSpdUp;
                    break;
                }
            case 2:
                {
                    if (sm.passivesRow1[1].enabled) critChance = critChanceUp;  
                    break;
                }
            case 3:
                {
                    if (sm.passivesRow1[2].enabled) attackSpeed = attackSpeedUp;
                    break;
                }
            case 4:
                {
                    if (sm.chargeSkills[0].enabled)    
                    {
                        for (int i = 0; i < 3; i++) player.GetComponent<ChargeAttacks>().activeBools[i] = false;
                        player.GetComponent<ChargeAttacks>().activeBools[0] = true;

                        player.GetComponent<ChargeAttacks>().activeSkill = 0;
                    }
                    break;
                }
            case 5:
                {
                    if (sm.chargeSkills[1].enabled)      
                    {
                        for (int i = 0; i < 3; i++) player.GetComponent<ChargeAttacks>().activeBools[i] = false;
                        player.GetComponent<ChargeAttacks>().activeBools[1] = true;

                        player.GetComponent<ChargeAttacks>().activeSkill = 1;
                    }
                    break;
                }
            case 6:
                {
                    if (sm.chargeSkills[2].enabled)       
                    {
                        for (int i = 0; i < 3; i++) player.GetComponent<ChargeAttacks>().activeBools[i] = false;
                        player.GetComponent<ChargeAttacks>().activeBools[2] = true;

                        player.GetComponent<ChargeAttacks>().activeSkill = 2;
                    }
                    break;
                }
            case 7:
                {
                    if (sm.passivesRow2[0].enabled) critDmg = critDmgUp;   
                    break;
                }
            case 8:
                {
                    if (sm.passivesRow2[1].enabled) blockChance = blockChanceUp;
                    break;
                }
            case 9:
                {
                    if (sm.passivesRow2[2].enabled) stunChance = stunChanceUp;
                    break;
                }
        }
    }
}
