﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public static PlayerQuest instance;

    public int CurrentExperience { get; set; }

    public int activeQuests = 0, completedQuests = 0;

    public List<Quest> quest = new List<Quest>();
    public List<Quest> finishedQuests = new List<Quest>();


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        //Checks for enemy death
        CombatEvent.OnEnemyDeath += EnemyToExperience;
    }

    private void Update()
    {
        if(activeQuests > GetComponent<Journal>().activeButtons.Count)
        {
            //add
            GetComponent<Journal>().addButton(quest[quest.Count - 1]);
        }
    }

    //Gives exp for kill and checks if it was for a quest
    public void EnemyToExperience(IEnemy enemy)
    {
        GrantExperience(enemy.Experience);

        for (int i = 0; i < activeQuests; i++)
        {
            if (quest[i].isActive && enemy.ID == quest[i].goal.requiredID)
            {
                quest[i].goal.EnemyKilled();
                if (quest[i].goal.IsReached())
                {
                    quest[i].Complete();
                }
            }
        }
    }

    public void GrantExperience(int amount)
    {
        CurrentExperience += amount;
        Debug.Log("Current Exp: " + CurrentExperience);
    }

    //Method that looks if the quest items have been collected
    public void newItemCollected(int itemID)
    {
        for (int i = 0; i < activeQuests; i++)
        {
            if (itemID == quest[i].goal.requiredID)
            {
                if (quest[i].isActive)
                {
                    quest[i].goal.ItemCollected();
                }
                if (quest[i].goal.IsReached())
                {
                    quest[i].Complete();
                }
            }
        }
    }
    


}