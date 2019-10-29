using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertInteraction : MonoBehaviour
{
    KeyCode interactionButton;
    float time, timeReset;

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
            }
        }
    }


}
