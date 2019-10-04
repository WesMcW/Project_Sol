using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerQuest player;

    public Text titleText, descriptionText;

    public bool multipleQuests = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (quest != null && !quest.helped && !quest.isActive)
            {
                titleText.text = quest.title;
                descriptionText.text = quest.description;
            }
            else if (quest != null && quest.isActive && !quest.helped)
            {
                titleText.text = null;
                descriptionText.text = "What are you waiting for?";
            }
            else if (quest != null && quest.finished && quest.helped && quest.isActive)
            {
                descriptionText.text = "Thank you so much!!";
                player.CurrentExperience += quest.experienceReward;
                //quest.resetQuests();
                player.activeQuests--;
                player.quest.Remove(quest);
                player.finishedQuests.Add(quest);
                Debug.Log("Current Exp: " + player.CurrentExperience);

                if (multipleQuests)
                {
                    GetComponent<MultiQ>().currentQ++;
                }
                quest = null;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && !quest.finished && !quest.isActive)
        {
            quest.isActive = true;
            player.activeQuests++;
            player.quest.Add(quest);
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        titleText.text = null;
        descriptionText.text = null;
    }

}
