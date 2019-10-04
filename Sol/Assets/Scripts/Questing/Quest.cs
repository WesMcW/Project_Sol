using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest.asset", menuName = "Quest")]

public class Quest : ScriptableObject
{
    public bool isActive, helped, finished = false;

    public string title;
    public string description;
    public int experienceReward;

    public QuestGoal goal;

    public void Complete()
    {
        helped = true;
        finished = true;
    }

    public void resetQuests()
    {
        isActive = false;
        helped = false;
        finished = false;
        goal.currentAmount = 0;
    }
}
