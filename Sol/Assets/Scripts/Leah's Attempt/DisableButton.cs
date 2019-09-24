using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButton : MonoBehaviour
{
    // disables button once it is clicked
    // should be on all passive skill buttons

    Animator anim;
    public bool buttonActive = false;
    public bool buyable = false;
    public bool skillActive = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameObject.GetComponent<Button>().onClick.AddListener(turnOff);
    }

    private void Update()
    {
        anim.SetBool("on", buttonActive);
        anim.SetBool("buyable", buyable);       // maybe?
        anim.SetBool("bought", skillActive);
    }

    void turnOff()
    {
        if (skillActive) gameObject.GetComponent<Button>().interactable = false;
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
