using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuest : MonoBehaviour
{
    public int CurrentExperience { get; set; }

    public int activeQuests = 0, completedQuests = 0;

    public List<Quest> quest = new List<Quest>();
    public List<Quest> finishedQuests = new List<Quest>();

    private void Start()
    {
        CombatEvent.OnEnemyDeath += EnemyToExperience;

        foreach(Quest a in quest)
        {
            a.resetQuests();
        }
        foreach (Quest a in finishedQuests)
        {
            a.resetQuests();
        }
    }

    private void Update()
    {
        if(activeQuests < GetComponent<Journal>().activeButtons.Count)
        {
            //subtract
        }
        else if(activeQuests > GetComponent<Journal>().activeButtons.Count)
        {
            //add
            GetComponent<Journal>().addButton(quest[quest.Count - 1]);
        }
    }

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < activeQuests; i++)
        {
            if (collision.CompareTag("Item"))
            {
                int item = collision.GetComponent<ItemInfo>().ItemID;
                //Debug.Log(item);

                if (quest[i].isActive && item == quest[i].goal.requiredID)
                {
                    quest[i].goal.ItemCollected();
                    //Destroy(collision.gameObject);
                }
                if (quest[i].goal.IsReached())
                {
                    quest[i].Complete();
                }
            }
        }
    }
}