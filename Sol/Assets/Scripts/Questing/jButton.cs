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

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = desc;
        reqAmtText.text = "Needed: " + reqAmt.ToString();
        currAmtText.text = "Have: " + currAmt.ToString();
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
