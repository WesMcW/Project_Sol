using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "skill", menuName = "Skill")]
public abstract class Skill : ScriptableObject
{
    public SkillType type;
    public Button button;
    public string description;
    public int cost;
    public bool on;
    public bool enabled;
    

    public Skill(int c)
    {
        cost = c;
        on = false;
        enabled = false;
    }

    public virtual bool buySkill(int balance)
    {
        if (!enabled && balance >= cost)
        {
            balance -= cost;
            enabled = true;
            setBought();
            button.GetComponent<Animator>().SetBool("bought", true);
            Debug.Log("Successfully bought: " + name);
            return true;
        }
        else Debug.Log("Need more skill points");
        return false;
    }

        // animation methods
    public void turnButtonOn()
    {
        on = true;
        button.GetComponent<Animator>().SetBool("on", true);
    }
    public void setBuyable()
    {
        button.GetComponent<Animator>().SetBool("buyable", true);
    }
    public void setUnbuyable()
    {
        button.GetComponent<Animator>().SetBool("buyable", false);
    }
    public void setBought()
    {
        button.GetComponent<Animator>().SetBool("bought", true);
    }
}

[CreateAssetMenu(fileName = "passiveSkill", menuName = "Passive Skill", order = 51)]
public class PassiveSkill : Skill
{
    public PassiveSkill(int c) : base(c)
    {
        enabled = false;
    }

    public override bool buySkill(int balance)
    {
        if (!enabled && balance >= cost)
        {
            balance -= cost;
            enabled = true;
            button.GetComponent<Animator>().SetBool("bought", true);
            Debug.Log("Successfully bought: " + name);
            setBought();
            button.interactable = false;
            return true;
        }
        else Debug.Log("Need more skill points");
        return false;
    }
}

[CreateAssetMenu(fileName = "activeSkill", menuName = "Active Skill", order = 51)]
public class ActiveSkill : Skill
{
    public bool active;
    public ActionType actionType;

    public ActiveSkill(int c) : base(c)
    {
        enabled = false;
        active = false;
    }

    public void activateSkill()
    {
        button.GetComponent<Animator>().SetBool("active", true);
        active = true;
    }

    public void deactivateSkill()
    {
        active = false;
    }

    public void setUnactiveAnim()
    {
        button.GetComponent<Animator>().SetBool("active", false);
    }
}

public enum SkillType
{
    Passive,
    Active
}

public enum ActionType
{
    Dodge,
    Charge,
    SpecialDodge
}