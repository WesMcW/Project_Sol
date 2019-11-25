using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Journal : MonoBehaviour
{
    public static Journal inst;

    public GameObject journal, active, complete;
    public Text buttDesc;
    public bool open;
    public Button buttonPrefab;

    private PlayerQuest PQ;

    public List<Button> activeButtons;
    public List<Button> completedButtons;

    private void Awake()
    {
        if (inst != null) Destroy(this);
        else inst = this;
    }

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
    }

    public void openJ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !open)
        {
            journal.SetActive(true);
            open = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && open)
        {
            journal.SetActive(false);
            open = false;
        }
    }

    //Creates buttons in the journal for active quests
    public void addButton(Quest q)
    {
        Button temp;
        temp = Instantiate(buttonPrefab, journal.transform.position, Quaternion.identity);
        temp.transform.SetParent(active.transform);

        activeButtons.Add(temp);

        temp.GetComponent<jButton>().title = q.title;
        temp.GetComponent<jButton>().desc = q.description;
        temp.GetComponent<jButton>().descriptionText = buttDesc;

        q.button = temp;
    }

    //Addes button to completed 
    public void removeButton(Quest q)
    {
        completedButtons.Add(q.button);
        activeButtons.Remove(q.button);
        q.button.transform.SetParent(complete.transform);
    }

}
