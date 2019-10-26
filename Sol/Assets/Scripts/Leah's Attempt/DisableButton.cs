using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
    public void makeInteractable()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void notInteractable()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
}
