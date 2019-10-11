using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public GameObject journal, active;
    public Text buttDesc;
    public bool open;
    public Button buttonPrefab;

    private PlayerQuest PQ;

    public List<Button> activeButtons;
    public List<Button> completedButtons;


    void Start()
    {
        PQ = GetComponent<PlayerQuest>();
        if(journal != null) journal.SetActive(false);
        activeButtons = new List<Button>();
        completedButtons = new List<Button>();
    }

    void Update()
    {
        openJ();
        //updateJ();
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

    /*
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
    */

    public void addButton(Quest q)
    {
        Button temp;
        temp = Instantiate(buttonPrefab, journal.transform.position, Quaternion.identity);
        temp.transform.SetParent(active.transform);

        activeButtons.Add(temp);

        //temp.GetComponent<ScrollRect>().viewport = active.GetComponent<RectTransform>();

        temp.GetComponent<jButton>().title = q.title;
        temp.GetComponent<jButton>().desc = q.description;
        temp.GetComponent<jButton>().descriptionText = buttDesc;
    }

}
