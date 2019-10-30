﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertInteraction : MonoBehaviour
{
    KeyCode interactionButton;
    float time, timeReset;
    /*
     * This script is only running when the alert is turned on. 
     * It will check for player input on an interactable object
     * (interactable objects must have a class which DERIVES from Interactable)
     * 
     */
    private void Start()
    {
        time = InteractionManager.IM.timeDelay;
        timeReset = time;
        interactionButton = InteractionManager.IM.GetInteractionButton();
    }

    private void Update()
    {
        if (Input.GetKey(interactionButton))
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                if (!InteractionManager.IM.IsInteracting())
                {
                    InteractionManager.IM.StartInteracting();
                }
                time = timeReset;
            }
        }
    }


}