using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
   /* public bool IsActiveSkill;
    public bool buttonActive = false;
    public bool skillActive = false;

    void Start()
    {
        if (!IsActiveSkill) gameObject.GetComponent<Button>().onClick.AddListener(turnOff);
    }

    void turnOff()
    {
        if (skillActive) gameObject.GetComponent<Button>().interactable = false;
    }
    */
    public void makeInteractable()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void notInteractable()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
}
