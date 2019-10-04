using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiQ : MonoBehaviour
{
    public Quest[] quests;
    public int currentQ;

    private int i;

    void Start()
    {
        GetComponent<QuestGiver>().quest = quests[0];
        i = currentQ;

        foreach(Quest a in quests)
        {
            a.resetQuests();
        }
    }

    void Update()
    {
        if (i != currentQ && currentQ < quests.Length)
        {
            GetComponent<QuestGiver>().quest = quests[currentQ];
            i = currentQ;
        }
    }
}
