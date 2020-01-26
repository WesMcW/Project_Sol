using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsManager : MonoBehaviour
{
    public static SkillsManager inst; 

    public int skillPoints = 0, skillsBought = 0;
    int mon, sCount;
    public Text nameTxt, descTxt, costTxt;
    public Image skillImg;
    SetSkills set;
    bool isVisible = false;

    public string[] buttonColors;
    public Skill[] allSkills;
    //public PassiveSkill[] passivesRow1, passivesRow2;

    [Header("All Skill Buttons")]
    public Button[] allButtons;

    [Header("Active Skill Arrays")]
    public ActiveSkill[] dodgeRoll;
    public ActiveSkill[] chargeSkills;

    [Header("Passive Skill Arrays")]
    public PassiveSkill[] passivesRow1;
    public PassiveSkill[] passivesRow2;

    private void Awake()
    {
        if (inst != null) Destroy(this);
        else inst = this;
    }

    void Start()
    {
        set = GetComponent<SetSkills>();
        buttonColors = new string[10];

        resetButton();
        setSkillButtons();
        dodgeRoll[0].turnButtonOn();
        mon = skillPoints;
        sCount = skillsBought;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            //Debug.Log("Manually added skill point");
            skillPoints++;
        }
        if (Input.GetKeyDown(KeyCode.P)) showSkills();

        if (sCount != skillsBought) onSkills();
        if (mon != skillPoints) checkPrices();

        sCount = skillsBought;
        mon = skillPoints;
    }

    void showSkills()
    {
        if (isVisible) skillImg.gameObject.SetActive(false);
        else skillImg.gameObject.SetActive(true);
        isVisible = !isVisible;
    }

    bool enablePassive(Skill skill)
    {
        if(!skill.enabled)
        {
            skillsBought++;
            if (skill.buySkill(skillPoints)) skillPoints -= skill.cost;
            checkPrices();
            return true;
        }
        return false;
    }

    bool enableActive(ActiveSkill[] array, int index)
    {
        if (!array[index].enabled)
        {
            if (array[index].buySkill(skillPoints))
            {
                if(array != dodgeRoll) buttonColors[index + 4] = "activeStatic";
                skillsBought++;
                skillPoints -= array[index].cost;
                checkPrices();

                bool firstActive = true;
                foreach (ActiveSkill a in array) if (a.active) firstActive = false;    // if no skills are active, activate this one
                if (firstActive) array[index].activateSkill();
                return firstActive;
            }
            return false;
        }

        else
        {
            buttonColors[4] = "boughtStatic";
            buttonColors[5] = "boughtStatic";
            buttonColors[6] = "boughtStatic";

            foreach (ActiveSkill a in array)
            {
                buttonColors[index + 4] = "activeStatic";
                if (a.active) a.deactivateSkill(); // deactivate all actives, activate this one
                if (a != array[index]) a.setUnactiveAnim();
            }
            array[index].activateSkill();
            return true;
        }
    }


    void setText(Skill skill)
    {
        string desc = skill.description.Replace("@", System.Environment.NewLine);
        nameTxt.text = skill.name;
        descTxt.text = desc;
        if (costTxt != null) costTxt.text = "Cost: " + skill.cost;
    }

    public void noText()
    {
        nameTxt.text = "";
        descTxt.text = "";
        if (costTxt != null) costTxt.text = "";
    }

    public void checkPrices()
    {
        //Debug.Log("checking prices...");
        for(int i = 0; i < allSkills.Length; i++)
        {
            if (allSkills[i].on && !allSkills[i].enabled)
            {
                if (allSkills[i].cost <= skillPoints)
                {
                    buttonColors[i] = "buyableStatic";
                    allSkills[i].setBuyable();
                }
                else
                {
                    buttonColors[i] = "moreMoneyStatic";
                    allSkills[i].setUnbuyable();
                }
            }
        }
    }

    void onSkills()
    {
        bool turnedOn = false;
        if(skillsBought > 0 && !passivesRow1[0].on)
        {
            turnedOn = true;
            for(int i = 0; i < passivesRow1.Length; i++)
            {
                passivesRow1[i].turnButtonOn();
                buttonColors[i + 1] = "moreMoneyStatic";
            }
        }
        else if (skillsBought > 2 && !chargeSkills[0].on)
        {
            turnedOn = true;
            for (int i = 0; i < chargeSkills.Length; i++)
            {
                chargeSkills[i].turnButtonOn();
                buttonColors[i + 4] = "moreMoneyStatic";
            }
        }
        else if(skillsBought > 4 && !passivesRow2[0].on)
        {
            turnedOn = true;
            for (int i = 0; i < passivesRow2.Length; i++)
            {
                passivesRow2[i].turnButtonOn();
                buttonColors[i + 7] = "moreMoneyStatic";
            }
        }

        //if (turnedOn) checkPrices();
    }

    void setSkillButtons()
    {
        allSkills = new Skill[allButtons.Length];
        allSkills[0] = dodgeRoll[0];
        for (int i = 0; i < 3; i++) allSkills[i + 1] = passivesRow1[i];
        for (int i = 0; i < 3; i++) allSkills[i + 4] = chargeSkills[i];
        for (int i = 0; i < 3; i++) allSkills[i + 7] = passivesRow2[i];

        for (int i = 0; i < allSkills.Length; i++)
        {
            allSkills[i].button = allButtons[i];
            allButtons[i].interactable = false;
        }
        dodgeRoll[0].button.interactable = true;
    }

    #region Button Methods

    public void dodgeBtn()
    {
        if (enableActive(dodgeRoll, 0))
        {
            set.enableSkill(0);
            buttonColors[0] = "activeStatic";
        }
    }

    public void showText(int index){ setText(allSkills[index]); }
    public void buyPassive(int index)
    {
        if (enablePassive(allSkills[index]))
        {
            buttonColors[index] = "boughtStatic";
            set.enableSkill(index);
        }
    }
    public void buyActive(int index)
    {
        // this will only apply for charge attacks right now, needs charge attack index
        if (enableActive(chargeSkills, index)) set.enableSkill(index + 4);
    }


    public void resetButton()
    {
        //Debug.Log("Reseting scriptable objects.");

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

        foreach (PassiveSkill a in passivesRow2)
        {
            a.enabled = false;
            a.on = false;
        }
    }

    #endregion
}
