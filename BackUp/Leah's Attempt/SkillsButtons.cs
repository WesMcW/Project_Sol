using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsButtons : MonoBehaviour
{
    int skillsBought, spRef;
    bool newPoint = false;
    public bool skillTreeOpen = false;
    public Image skillTree;
    public Button dodgeBtn;
    public Button[] line1, line2, line3;

    // Start is called before the first frame update
    void Start()
    {
        spRef = GetComponent<SkillsList>().spRef;
        skillsBought = GetComponent<SkillsList>().skillsCount;
        dodgeBtn.GetComponent<DisableButton>().buttonActive = true;
        skillTree.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (skillsBought != GetComponent<SkillsList>().skillsCount) // checking if a point was used
        {
            skillsBought = GetComponent<SkillsList>().skillsCount;
            newPoint = true;
        }
        if(spRef != GetComponent<SkillsList>().spRef)   // checking if a point was gained
        {
            spRef = GetComponent<SkillsList>().spRef;
            if (skillsBought == 0 && spRef == 1) dodgeBtn.GetComponent<DisableButton>().buyable = true;
            else
            {
                    // setting buttons to buyable or not
                if (skillsBought > 0)
                {
                    if (GetComponent<SkillsList>().allSkills[0].cost <= spRef)
                    {
                        foreach (Button a in line1)
                        {
                            if (!a.GetComponent<DisableButton>().skillActive)
                            {
                                a.GetComponent<DisableButton>().buyable = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (Button a in line1)
                        {
                            if (!a.GetComponent<DisableButton>().skillActive) a.GetComponent<DisableButton>().buyable = false;
                        }
                    }

                    if (skillsBought >= 3)
                    {
                        if (GetComponent<SkillsList>().allSkills[3].cost <= spRef)
                        {
                            foreach (Button a in line2)
                            {
                                if (!a.GetComponent<DisableButton>().skillActive)
                                {
                                    a.GetComponent<DisableButton>().buyable = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (Button a in line2)
                            {
                                if (!a.GetComponent<DisableButton>().skillActive) a.GetComponent<DisableButton>().buyable = false;
                            }
                        }

                        if(skillsBought >= 5)
                        {
                            if (GetComponent<SkillsList>().allSkills[6].cost <= spRef)
                            {
                                foreach (Button a in line3)
                                {
                                    if (!a.GetComponent<DisableButton>().skillActive)
                                    {
                                        a.GetComponent<DisableButton>().buyable = true;
                                    }
                                }
                            }
                            else
                            {
                                foreach (Button a in line3)
                                {
                                    if (!a.GetComponent<DisableButton>().skillActive) a.GetComponent<DisableButton>().buyable = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P)) skillTreeOpen = !skillTreeOpen;

        if (skillTreeOpen)
        {
            skillTree.gameObject.SetActive(true);
            if (newPoint)
            {
                if (GetComponent<StatsManager>().player.GetComponent<Player>().canDodge) enableRow(line1);
                if (skillsBought >= 3) enableRow(line2);
                if (skillsBought >= 5) enableRow(line3);

                newPoint = false;
            }
        }
        else
        {
            skillTree.gameObject.SetActive(false);
        }
    }

    void enableRow(Button[] line)
    {
        foreach (Button a in line)
        {
            a.GetComponent<DisableButton>().buttonActive = true;
        }
    }
}
