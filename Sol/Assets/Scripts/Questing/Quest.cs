using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Quest.asset", menuName = "Quest")]

public class Quest : ScriptableObject
{
    public bool isActive, helped, take, notTurnedInHere, finished = false;

    //This is used in "QuestBehavior" for when the quest is not turned in to the NPC who assigned it.
    public Animator questGiver;

    public string title;
    public string description;
    public int experienceReward, questID;
    public QuestGoal goal;
    public PlayerQuest player;
    public Button button;

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
