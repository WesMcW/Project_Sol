using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class jButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Sets the quest description in the journal if quest is hovered on. 

    public string title, desc;
    public Text descriptionText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = desc;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = null;
    }

    void Start()
    {
        GetComponent<Button>().interactable = false;
        //Sets the text on the Button
        transform.Find("Text").GetComponent<Text>().text = title;
    }
}
