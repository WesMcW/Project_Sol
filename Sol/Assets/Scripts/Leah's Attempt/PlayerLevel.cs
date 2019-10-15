﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    SkillsManager sm;
    public static PlayerLevel inst;

    public bool firstBoss = false, leveled = false;
    public int level = 1, xp = 0;
    public int xpToLevel = 100;
    float xpScaler = 1.5F;

    private void Awake()
    {
        if (inst != null) Destroy(this);
        else inst = this;
        sm = SkillsManager.inst;
    }

    void Update()
    {
        if (level == 1 && firstBoss)
        {
            level = 2;
            sm.skillPoints++;
        }

        if(level > 1)
        {
            if (xp >= xpToLevel) leveled = true;
        }

        if (leveled)
        {
            level++;
            sm.skillPoints++;
            xp -= xpToLevel;
            xpToLevel = Mathf.RoundToInt(xpToLevel * xpScaler);
            xpScaler += 0.5F;

            leveled = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("XP gained.");
            xp++;
        }
    }
}
