using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateColorSkills : MonoBehaviour
{
    SkillsManager sm;
    public string[] currentColor;
    int[] chargeIndexs;
    bool hasOpened = false;

    private void Start()
    {
        chargeIndexs = new int[4] { 0, 4, 5, 6 };
    }

    private void OnEnable()
    {
        if (!hasOpened)
        {
            sm = SkillsManager.inst;
            currentColor = new string[10];

            for (int i = 0; i < sm.allSkills.Length; i++)
            {
                sm.buttonColors[i] = sm.allSkills[i].button.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
            }

            sm.dodgeRoll[0].turnButtonOn();
            sm.buttonColors[0] = "moreMoneyStatic";
            hasOpened = true;

        }
        else
        {
            for(int i = 0; i < chargeIndexs.Length; i++)
            {
                if (currentColor[chargeIndexs[i]] == "activeStatic") sm.allButtons[chargeIndexs[i]].GetComponent<Animator>().SetBool("active", true);
            }

            for(int i = 0; i < sm.allSkills.Length; i++)
            {
                sm.allSkills[i].button.GetComponent<Animator>().Play(currentColor[i]);
            }
        }

        SkillsManager.inst.checkPrices();
    }

    public void updateColorArray(int index, string animName)
    {
        currentColor[index] = animName;
    }

    private void OnDisable()
    {
        foreach (string a in sm.buttonColors) currentColor = sm.buttonColors;
    }
}
