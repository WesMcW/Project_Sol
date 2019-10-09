﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public int money = 0, skillsBought = 0;
    int sCount;
    public Text nameTxt, descTxt;
    SetSkills set;

    Skill[] allSkills;
    public PassiveSkill[] passivesRow1;

    [Header("All Skill Buttons")]
    public Button[] allButtons;

    [Header("Active Skill Arrays")]
    public ActiveSkill[] dodgeRoll;
    public ActiveSkill[] chargeSkills;

    void Start()
    {
        set = GetComponent<SetSkills>();

        resetButton();
        setSkillButtons();
        dodgeRoll[0].turnButtonOn();
        sCount = skillsBought;
    }

    void Update()
    {
        int mon = money;
        if (Input.GetKeyDown(KeyCode.Space)) money++;
        if (mon != money) checkPrices();

        if (sCount != skillsBought) onSkills();
        sCount = skillsBought;
    }


    void enablePassive(Skill skill)
    {
        if(!skill.enabled)
        {
            skillsBought++;
            if (skill.buySkill(money)) money -= skill.cost;
            checkPrices();
        }
    }

    void enableActive(ActiveSkill[] array, int index)
    {
        if (!array[index].enabled)
        {
            if (array[index].buySkill(money))
            {
                skillsBought++;
                money -= array[index].cost;
                checkPrices();
                bool anyActive = false;
                foreach (ActiveSkill a in array) if (a.active) anyActive = true;    // if no skills are active, activate this one
                if (!anyActive) array[index].activateSkill();
            }
        }

        else
        {
            foreach (ActiveSkill a in array)
            {
                if (a.active) a.deactivateSkill(); // deactivate all actives, activate this one
                if (a != array[index]) a.setUnactiveAnim();
            }
            array[index].activateSkill();
        }
    }


    void setText(Skill skill)
    {
        string desc = skill.description.Replace("@", System.Environment.NewLine);
        nameTxt.text = skill.name;
        descTxt.text = desc;
    }

    public void noText()
    {
        nameTxt.text = "";
        descTxt.text = "";
    }

    void checkPrices()
    {
        Debug.Log("checking prices...");
        foreach (Skill a in allSkills)
        {
            if (a.on && !a.enabled)
            {
                if (a.cost <= money) a.setBuyable();
                else a.setUnbuyable();
            }
        }
    }

    void onSkills()
    {
        if(skillsBought > 0 && !passivesRow1[0].on)
        {
            foreach (PassiveSkill a in passivesRow1) a.turnButtonOn();
        }
        else if (skillsBought > 2 && !chargeSkills[0].on)
        {
            foreach (ActiveSkill a in chargeSkills) a.turnButtonOn();
        }
    }

    void setSkillButtons()
    {
        allSkills = new Skill[allButtons.Length];
        allSkills[0] = dodgeRoll[0];
        for (int i = 0; i < 3; i++) allSkills[i + 1] = passivesRow1[i];
        for (int i = 0; i < 3; i++) allSkills[i + 4] = chargeSkills[i];

        for (int i = 0; i < allSkills.Length; i++) allSkills[i].button = allButtons[i];
    }

    #region Button Methods

    public void dodgeBtn()
    {
        enableActive(dodgeRoll, 0);
        set.enableDodge();
    }
    public void hoverOnDodgeBtn() { setText(dodgeRoll[0]); }

    //

    public void passiveBtn1() { enablePassive(passivesRow1[0]); }
    public void hoverOnPassive1Btn() { setText(passivesRow1[0]); }

    public void passiveBtn2() { enablePassive(passivesRow1[1]); }
    public void hoverOnPassive2Btn() { setText(passivesRow1[1]); }

    public void passiveBtn3() { enablePassive(passivesRow1[2]); }
    public void hoverOnPassive3Btn() { setText(passivesRow1[2]); }

    //

    public void chargeBtn1() { enableActive(chargeSkills, 0); }
    public void hoverOnCharge1Btn() { setText(chargeSkills[0]); }

    public void chargeBtn2() { enableActive(chargeSkills, 1); }
    public void hoverOnCharge2Btn() { setText(chargeSkills[1]); }

    public void chargeBtn3() { enableActive(chargeSkills, 2); }
    public void hoverOnCharge3Btn() { setText(chargeSkills[2]); }

    public void resetButton()
    {
        Debug.Log("Reseting scriptable objects.");

        dodgeRoll[0].enabled = false;
        dodgeRoll[0].on = false;
        dodgeRoll[0].active = false;

        foreach(PassiveSkill a in passivesRow1)
        {
            a.enabled = false;
            a.on = false;
        }

        foreach(ActiveSkill a in chargeSkills)  // change this later
        {
            a.enabled = false;
            a.on = false;
            a.active = false;
        }
    }

    #endregion
}