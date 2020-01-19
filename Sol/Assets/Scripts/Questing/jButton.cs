using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class jButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Sets the quest description in the journal if quest is hovered on. 

    public string title, desc;
    public int reqAmt, currAmt;
    public Text descriptionText, reqAmtText, currAmtText;
    public Quest myQuest;
    public bool isComplete = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isComplete && !myQuest.isActive)
        {
            isComplete = true;
        }

        if (!isComplete)
        {
            if (myQuest != null)
            {
                currAmt = myQuest.goal.currentAmount;
            }
            reqAmtText.text = "Needed: " + reqAmt.ToString();
            currAmtText.text = "Have: " + currAmt.ToString();
        }
        descriptionText.text = desc;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = null;
        reqAmtText.text = null;
        currAmtText.text = null;
    }

    void Start()
    {
        GetComponent<Button>().interactable = false;
        //Sets the text on the Button
        transform.Find("Text").GetComponent<Text>().text = title;
    }
}
