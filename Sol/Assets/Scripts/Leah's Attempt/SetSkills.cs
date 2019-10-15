﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkills : MonoBehaviour
{
    public GameObject player;
    SkillsManager sm;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sm = GetComponent<SkillsManager>();
    }

        // Each method will enable the skill it is attached to in SkillsManager

    public void enableDodge()
    {
        if (sm.dodgeRoll[0].active) player.GetComponent<Player>().canDodge = true;
    }
}
