using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    SkillsManager sm;
    public static PlayerLevel inst;

    public bool firstBoss = false, leveled = false;
    public int level = 1, xpRef = 0;    // main xp in PlayerQuest
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
        xpRef = PlayerQuest.instance.CurrentExperience;

        if (level == 1 && firstBoss)
        {
            level = 2;
            sm.skillPoints++;
        }

        if(level > 1)
        {
            if (xpRef >= xpToLevel) leveled = true;

            // add to xp bar
        }

        if (leveled)
        {
            level++;
            sm.skillPoints++;
            PlayerQuest.instance.CurrentExperience -= xpToLevel;
            xpToLevel = Mathf.RoundToInt(xpToLevel * xpScaler);
            xpScaler += 0.5F;

            leveled = false;
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            Debug.Log("XP gained.");
            PlayerQuest.instance.CurrentExperience++;
        }
    }
}
