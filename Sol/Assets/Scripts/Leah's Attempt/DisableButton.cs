using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    // disables button once it is clicked
    // should be on all passive skill buttons

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(turnOff);
    }

    void turnOff()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
}
