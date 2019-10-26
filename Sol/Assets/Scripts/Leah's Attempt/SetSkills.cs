using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkills : MonoBehaviour
{
    public GameObject player;
    SkillsManager sm;

    [Header("Stat Modifiers:")]
    public float speedUp;
    public float critChance;
    public float attackSpeed;
    public float critDmg;
    public float blockChance;
    public float stunChance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sm = GetComponent<SkillsManager>();
    }

    public void enableSkill(int x)  // less methods
    {
        Debug.Log("x is " + x);
        switch (x)
        {
            case 0:
                {
                    if (sm.dodgeRoll[0].active) player.GetComponent<Player>().canDodge = true;
                    break;
                }
            case 1:
                {
                    if (sm.passivesRow1[0].enabled) player.GetComponent<Player>().moveSpeed = speedUp;
                    break;
                }
            case 2:
                {
                    if (sm.passivesRow1[1].enabled) Debug.Log("Increase crit chance to " + critChance);     // cant do until damage is done
                    break;
                }
            case 3:
                {
                    if (sm.passivesRow1[2].enabled) Debug.Log("Increase attack speed to " + attackSpeed);   // ask first
                    break;
                }
            case 4:
                {
                    if (sm.chargeSkills[0].enabled)                 // gotta make these first
                    {
                        Debug.Log("Enable one, disable others");
                    }
                    break;
                }
            case 5:
                {
                    if (sm.chargeSkills[1].enabled)                 // gotta make these first
                    {
                        Debug.Log("Enable one, disable others");
                    }
                    break;
                }
            case 6:
                {
                    if (sm.chargeSkills[2].enabled)                 // gotta make these first
                    {
                        Debug.Log("Enable one, disable others");
                    }
                    break;
                }
            case 7:
                {
                    if (sm.passivesRow2[0].enabled) Debug.Log("Increase crit damage to " + critDmg);     // cant do until damage is done
                    break;
                }
            case 8:
                {
                    if (sm.passivesRow2[1].enabled) Debug.Log("Increase chance to block to " + blockChance);
                    break;
                }
            case 9:
                {
                    if (sm.passivesRow2[2].enabled) Debug.Log("Increase chance to stun to " + stunChance);
                    break;
                }
        }
    }
}
