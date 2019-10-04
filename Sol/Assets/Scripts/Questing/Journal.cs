using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public GameObject journal;
    public Text jTit, jTitFinished;
    public bool open;

    //private Quest quest;

    private PlayerQuest PQ;


    void Start()
    {
        PQ = GetComponent<PlayerQuest>();
        journal.SetActive(false);
    }

    void Update()
    {
        openJ();
        updateJ();
    }

    public void openJ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !open)
        {
            journal.SetActive(true);
            open = true;
            //updateJ();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && open)
        {
            journal.SetActive(false);
            open = false;
        }
    }

    public void updateJ()
    {
        List<string> tit = new List<string>();
        List<string> doneTit = new List<string>();

        for (int i = 0; i < PQ.quest.Count; i++)
        {
            tit.Add(PQ.quest[i].title);
        }
        jTit.text = string.Join("\n", tit);

        for (int i = 0; i < PQ.finishedQuests.Count; i++)
        {
            doneTit.Add(PQ.finishedQuests[i].title);
        }
        jTitFinished.text = string.Join("\n", doneTit);
    }
}
