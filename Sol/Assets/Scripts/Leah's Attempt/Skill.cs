using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Skill
{
    public int cost { get; private set; }
    public bool enabled { get; protected set; }

    public Skill(int c)
    {
        cost = c;
        enabled = false;
    }
}

public class PassiveSkill : Skill
{
    private Action setStat;

    public PassiveSkill(int c, Action s):base(c)
    {
        setStat = s;
    }

    public void buySkill()
    {
        if (!enabled)   // also needs to check if skillPoints >= points
        {
            setStat();
            enabled = true;
        }
    }
}

public class ActiveSkill : Skill
{
    //public Action ability;        // can I just check the active for each and change a bool in another script?
    public bool active { get; set; }

    public ActiveSkill(int c) : base(c)
    {
        active = false;
        //ability = a;
    }

    public void buySkill()  // also needs to check if skillPoints >= points
    {
        if (!enabled)
        {
            enabled = true;
            // if none of same type are active, activate this, going to do that in other code
            // maybe when this is bought, change the button method to activateSkill() ?
        }
    }

    public void activateSkill() // needs to also disable all other like skills
    {
        if (!active)
        {
            //ability();
            active = true;
        }
    }

    public void deactivateSkill()
    {
        if (active)
        {
            //ability();
            active = false;
        }
    }
}