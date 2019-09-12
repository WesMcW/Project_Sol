using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsList : MonoBehaviour
{
    /// <summary>
    ///   How to make new skills:
    ///     
    /// Passive:
    ///     p = new PassiveSkill( COST, { method of what the skill will do/change } )
    ///     
    /// Active:
    ///     a = new ActiveSkill( COST )
    ///     ** add to matching List of ActiveSkill
    ///     
    /// I have some examples of both skill types below, use them to make actual skills
    /// I need some movement/ ability scripts before I can really implement this    
    /// 
    /// </summary>

    [Header("Lists")]
    public List<PassiveSkill> passives; // ?
    public List<ActiveSkill> chargeAttacks;
    public List<ActiveSkill> newDodges;

    [Header("Active Skills")]
    ActiveSkill sonicJump, boomerangThrow, aThirdOne;
    ActiveSkill dodge1, dodge2, dodge3;

    [Header("Passive Skills")]
    PassiveSkill smSize1, smSize2;

    // Start is called before the first frame update
    void Start()
    {
            //Passive Skills
        smSize1 = new PassiveSkill(3, () =>
        {
            GameObject temp = GameObject.Find("thing");
            temp.transform.localScale = new Vector3(temp.transform.localScale.x - 0.1F, temp.transform.localScale.y - 0.1F);
        });
        smSize2 = new PassiveSkill(5, () =>
        {
            GameObject temp = GameObject.Find("thing");
            temp.transform.localScale = new Vector3(temp.transform.localScale.x - 0.1F, temp.transform.localScale.y - 0.1F);
        });

        passives = new List<PassiveSkill>();

            //Charge Attacks
        sonicJump = new ActiveSkill(5);
        boomerangThrow = new ActiveSkill(5);
        aThirdOne = new ActiveSkill(5);

        chargeAttacks = new List<ActiveSkill>();
        chargeAttacks.Add(sonicJump);
        chargeAttacks.Add(boomerangThrow);
        chargeAttacks.Add(aThirdOne);

            //Dodge Abilities
        dodge1 = new ActiveSkill(10);
        dodge2 = new ActiveSkill(10);
        dodge3 = new ActiveSkill(10);

        newDodges = new List<ActiveSkill>();
        newDodges.Add(dodge1);
        newDodges.Add(dodge2);
        newDodges.Add(dodge3);
    }

    #region Buttons

            // Passive Buttons

    public void passive1Btn()
    {
        smSize1.buySkill();
    }

    public void passive2Btn()
    {
        smSize2.buySkill();
    }

            // Charge Attacks

    public void sonicJumpBtn()
    {
        if (!sonicJump.enabled)
        {
            //buy it, check if any are active, if not activate it

            if (sonicJump.cost <= 100)  // replace 100 with skillPoints
            {
                sonicJump.buySkill();   // buy it, subtract points
                Debug.Log("bought Sonic Jump skill");

                bool check = false;
                foreach (ActiveSkill a in chargeAttacks)    // check if any skills of type are active
                {
                    check = a.active;
                    if (check) break;
                    Debug.Log(a.ToString() + " is disabled.");
                }
                if (!check)
                {
                    sonicJump.activateSkill();
                    Debug.Log("Sonic Jump has been enabled.");
                } // if no skills of type are active, activate it
            }
        }
        else
        {
            //unactivate everything, activate this

            foreach (ActiveSkill a in chargeAttacks)
            {
                a.deactivateSkill();
                Debug.Log(a.ToString() + " has been disabled.");
            }
            sonicJump.activateSkill();
            Debug.Log("Sonic Jump has been enabled.");
        }
    }


    public void boomerangBtn()
    {
        if (!boomerangThrow.enabled)
        {
            //buy it, check if any are active, if not activate it

            if (boomerangThrow.cost <= 100)  // replace 100 with skillPoints
            {
                boomerangThrow.buySkill();   // buy it
                Debug.Log("bought Boomerang Throw skill");

                bool check = false;
                foreach (ActiveSkill a in chargeAttacks)    // check if any skills of type are active
                {
                    check = a.active;
                    if (check) break;
                    Debug.Log(a.ToString() + " is disabled.");
                }
                if (!check)
                {
                    Debug.Log("Boomerang Throw has been enabled.");
                    boomerangThrow.activateSkill();  // if no skills of type are active, activate it
                }
            }
        }
        else
        {
            //unactivate everything, activate this

            foreach (ActiveSkill a in chargeAttacks)
            {
                a.deactivateSkill();
                Debug.Log(a.ToString() + " has been disabled.");
            }
            boomerangThrow.activateSkill();
            Debug.Log("Boomerang Throw has been enabled.");
        }
    }


    public void thirdBtn()
    {
        if (!aThirdOne.enabled)
        {
            //buy it, check if any are active, if not activate it

            if (aThirdOne.cost <= 100)  // replace 100 with skillPoints
            {
                aThirdOne.buySkill();   // buy it
                Debug.Log("bought Other skill");

                bool check = false;
                foreach (ActiveSkill a in chargeAttacks)    // check if any skills of type are active
                {
                    check = a.active;
                    if (check) break;
                    Debug.Log(a.ToString() + " is disabled.");
                }
                if (!check)
                {
                    Debug.Log("Other has been enabled.");
                    aThirdOne.activateSkill();  // if no skills of type are active, activate it
                }
            }
        }
        else
        {
            //unactivate everything, activate this

            foreach (ActiveSkill a in chargeAttacks)
            {
                a.deactivateSkill();
                Debug.Log(a.ToString() + " has been disabled.");
            }
            aThirdOne.activateSkill();
            Debug.Log("Other has been enabled.");
        }
    }

            // Dodges   will add these when the rest work better

    #endregion
}
