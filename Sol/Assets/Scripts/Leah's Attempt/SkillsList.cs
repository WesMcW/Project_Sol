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
    ///     ** put DisableButton script on all passive buttons **
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
    public List<Skill> allSkills;
    public List<ActiveSkill> chargeAttacks;
    public List<ActiveSkill> newDodges;

    [Header("Active Skills")]
    ActiveSkill sonicJump, boomerangThrow, aThirdOne;
    ActiveSkill dodge1, dodge2, dodge3;

    [Header("Passive Skills")]
    PassiveSkill dodgeRoll;
    PassiveSkill speedUp, rollSpeedUp, passive3, passive4, passive5, passive6;

    public GameObject lastBtnClicked;      // disables buttons after purchase
    public int spRef;  // in StatsManager
    public int skillsCount;

    void Start()
    {
        //Passive Skills
        dodgeRoll = new PassiveSkill(1, () => GetComponent<StatsManager>().player.GetComponent<Player>().canDodge = true);
        speedUp = new PassiveSkill(1, () => GetComponent<StatsManager>().speedRef += 2F);
        rollSpeedUp = new PassiveSkill(1, () => GetComponent<StatsManager>().rollSpeedRef += 5F);
        passive3 = new PassiveSkill(1, () => Debug.Log("skill bought"));
        passive4 = new PassiveSkill(1, () => Debug.Log("skill bought"));
        passive5 = new PassiveSkill(1, () => Debug.Log("skill bought"));
        passive6 = new PassiveSkill(1, () => Debug.Log("skill bought"));

            //Charge Attacks
        sonicJump = new ActiveSkill(2);
        boomerangThrow = new ActiveSkill(2);
        aThirdOne = new ActiveSkill(2);

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

            // Add all skills to list
        allSkills = new List<Skill>();
        allSkills.Add(speedUp);
        allSkills.Add(rollSpeedUp);
        allSkills.Add(passive3);
        allSkills.Add(passive4);
        allSkills.Add(passive5);
        allSkills.Add(passive6);

        allSkills.Add(sonicJump);
        allSkills.Add(boomerangThrow);
        allSkills.Add(aThirdOne);
        allSkills.Add(dodge1);
        allSkills.Add(dodge2);
        allSkills.Add(dodge3);
    }

    void Update()
    {
        spRef = GetComponent<StatsManager>().skillPoints;
        lastBtnClicked = EventSystem.current.currentSelectedGameObject;
    }

    #region Buttons

    #region Passive Buttons

    public void dodgeBtn()
    {
        if (dodgeRoll.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= dodgeRoll.cost;
            skillsCount++;
            GetComponent<StatsManager>().skillPoints = dodgeRoll.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
            GetComponent<StatsManager>().updateStats();
        }
        else Debug.Log("Need more skill points");
    }

    public void speedUpBtn()
    {
        if (speedUp.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= speedUp.cost;
            skillsCount++;
            GetComponent<StatsManager>().skillPoints = speedUp.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
            GetComponent<StatsManager>().updateStats();
        }
        else Debug.Log("Need more skill points");
    }

    public void rollSpeedUpBtn()
    {
        if (rollSpeedUp.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= rollSpeedUp.cost;
            skillsCount++;
            GetComponent<StatsManager>().skillPoints = rollSpeedUp.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
            GetComponent<StatsManager>().updateStats();
        }
        else Debug.Log("Need more skill points");
    }

    public void passive3Btn()
    {
        if (passive3.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= passive3.cost;
            skillsCount++;
            GetComponent<StatsManager>().skillPoints = passive3.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
            GetComponent<StatsManager>().updateStats();
        }
        else Debug.Log("Need more skill points");
    }

    public void passive4Btn()
    {
        if (passive4.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= passive4.cost;
            skillsCount++;
            GetComponent<StatsManager>().skillPoints = passive4.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
            GetComponent<StatsManager>().updateStats();
        }
        else Debug.Log("Need more skill points");
    }

    public void passive5Btn()
    {
        if (passive5.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= passive5.cost;
            skillsCount++;
            GetComponent<StatsManager>().skillPoints = passive5.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
            GetComponent<StatsManager>().updateStats();
        }
        else Debug.Log("Need more skill points");
    }

    public void passive6Btn()
    {
        if (passive6.cost <= spRef)
        {
            GetComponent<StatsManager>().skillPoints -= passive6.cost;
            skillsCount++;
            GetComponent<StatsManager>().skillPoints = passive6.buySkill(spRef);
            lastBtnClicked.GetComponent<DisableButton>().skillActive = true;
            GetComponent<StatsManager>().updateStats();
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
                lastBtnClicked.GetComponent<DisableButton>().skillActive = true;

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
                    lastBtnClicked.GetComponent<DisableButton>().actionActive = true;
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
                if (a.active) a.deactivateSkill();
                Debug.Log(a.ToString() + " has been disabled.");
            }

            GetComponent<SkillsButtons>().line3[1].GetComponent<DisableButton>().actionActive = false;  // I'd like this better
            GetComponent<SkillsButtons>().line3[2].GetComponent<DisableButton>().actionActive = false;

            lastBtnClicked.GetComponent<DisableButton>().actionActive = true;
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
                lastBtnClicked.GetComponent<DisableButton>().skillActive = true;

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
                    lastBtnClicked.GetComponent<DisableButton>().actionActive = true;
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

            GetComponent<SkillsButtons>().line3[0].GetComponent<DisableButton>().actionActive = false;
            GetComponent<SkillsButtons>().line3[2].GetComponent<DisableButton>().actionActive = false;

            lastBtnClicked.GetComponent<DisableButton>().actionActive = true;
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
                lastBtnClicked.GetComponent<DisableButton>().skillActive = true;

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
                    lastBtnClicked.GetComponent<DisableButton>().actionActive = true;
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
            GetComponent<SkillsButtons>().line3[0].GetComponent<DisableButton>().actionActive = false;  // I'd like this better
            GetComponent<SkillsButtons>().line3[1].GetComponent<DisableButton>().actionActive = false;

            lastBtnClicked.GetComponent<DisableButton>().actionActive = true;
            Debug.Log("Other has been enabled.");
        }
    }

    // Dodges   will add these when dodge exists/this connects with player movement

    #endregion

    #endregion
}
