using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillsList : MonoBehaviour
{
    /// <summary>
    ///   How to make new skills:
    ///     
    /// Passive:
    ///     p = new PassiveSkill( COST, { () => method of what the skill will do/change } );
    ///     passives.Add(p);
    ///     
    ///     Then add the button function: basically duplicate existing button method and update 'p'
    ///     
    /// Active:
    ///     a = new ActiveSkill( COST );
    ///     SKILLTYPELIST.Add(a);
    ///     
    ///     Then add button function: same as passive buttons; duplicate and update 'a'
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
    PassiveSkill p, pEx2;

    GameObject lastBtnClicked;      // disables buttons after purchase
    int spRef;  // in StatsManager

    void Start()
    {
            //Passive Skills
        p = new PassiveSkill(1, () => Debug.Log("This is an example.")); 
        pEx2 = new PassiveSkill(3, () =>
        {
            Debug.Log("How to write multiple lines in here");
            Debug.Log("wow");
        });

        passives = new List<PassiveSkill>();
        passives.Add(p);
        passives.Add(pEx2);

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

    void Update()
    {
        spRef = GetComponent<StatsManager>().skillPoints;
        lastBtnClicked = EventSystem.current.currentSelectedGameObject;
    }

    #region Buttons

    #region Passive Buttons

    public void examplePassiveBtn()
    {
        if (p.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= p.cost;
            GetComponent<StatsManager>().skillPoints = p.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
        }
        else Debug.Log("Need more skill points");
    }

    #endregion

    #region Charge Attacks

    public void sonicJumpBtn()
    {
        if (!sonicJump.enabled)
        {
            //buy it, check if any are active, if not activate it

            if (sonicJump.cost <= spRef)
            {
                GetComponent<StatsManager>().skillPoints = sonicJump.buySkill(spRef);   // buy it, subtract points
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
            else Debug.Log("Need more skill points");
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
            if (boomerangThrow.cost <= spRef)
            {
                GetComponent<StatsManager>().skillPoints = boomerangThrow.buySkill(spRef);
                Debug.Log("bought Boomerang Throw skill");

                bool check = false;
                foreach (ActiveSkill a in chargeAttacks)
                {
                    check = a.active;
                    if (check) break;
                    Debug.Log(a.ToString() + " is disabled.");
                }
                if (!check)
                {
                    Debug.Log("Boomerang Throw has been enabled.");
                    boomerangThrow.activateSkill();
                }
            }
            else Debug.Log("Need more skill points");
        }
        else
        {
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
            if (aThirdOne.cost <= spRef)
            {
                GetComponent<StatsManager>().skillPoints = aThirdOne.buySkill(spRef);
                Debug.Log("bought Other skill");

                bool check = false;
                foreach (ActiveSkill a in chargeAttacks)
                {
                    check = a.active;
                    if (check) break;
                    Debug.Log(a.ToString() + " is disabled.");
                }
                if (!check)
                {
                    Debug.Log("Other has been enabled.");
                    aThirdOne.activateSkill();
                }
            }
            else Debug.Log("Need more skill points");
        }
        else
        {
            foreach (ActiveSkill a in chargeAttacks)
            {
                a.deactivateSkill();
                Debug.Log(a.ToString() + " has been disabled.");
            }
            aThirdOne.activateSkill();
            Debug.Log("Other has been enabled.");
        }
    }

    // Dodges   will add these when dodge exists/this connects with player movement

    #endregion

    #endregion
}
