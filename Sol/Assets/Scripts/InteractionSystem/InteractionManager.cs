using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] GameObject alert;
    Transform target;
    public static InteractionManager IM;
    [SerializeField] KeyCode interactionButton;
    public float timeDelay;
    bool interacting;



    private void Awake()
    {
        if(IM == null)
        {
            IM = this;
        } else
        {
            Destroy(this.gameObject);
        }
        alert.SetActive(false);
    }
    //Ensures the alerts show up at the correct places at the correct time and chooses the current interactable item

    /// <summary>
    /// Sets the new target for the alert and current interactable
    /// </summary>
    /// <param name="t"></param>
    public void SetTarget(Transform t)
    {
        target = t;

        if (t != null)
        {
            DisplayAlert();
        } else
        {
            TurnOffAlert();
        }
        
    }


    /// <summary>
    /// Display alert at target location
    /// </summary>
    void DisplayAlert()
    {
        alert.SetActive(true);
        SpriteRenderer sr = target.GetComponent<SpriteRenderer>() ?? target.parent.GetComponent<SpriteRenderer>();
        alert.transform.position = target.position + new Vector3(0, sr.bounds.size.y);
    }


    /// <summary>
    /// Turn off Alert GameObject
    /// </summary>
    public void TurnOffAlert()
    {
        alert.SetActive(false);
    }

    /// <summary>
    /// Turn on Alert GameObject
    /// </summary>
    public void TurnOnAlert()
    {
        alert.SetActive(true);
    }


   /// <summary>
   /// Returns the KeyCode for the Interaction Button
   /// </summary>
   /// <returns></returns>
    public KeyCode GetInteractionButton()
    {
        return interactionButton;
    }

    /// <summary>
    /// Start the Interaction
    /// </summary>
    public void StartInteracting()
    {
        interacting = true;
        Interactable theInt = target.GetComponent<Interactable>() ?? target.parent.GetComponent<Interactable>();
        theInt.Interact();
    }

    /// <summary>
    /// Returns if the Player is currently in an Interaction
    /// </summary>
    /// <returns></returns>
    public bool IsInteracting()
    {
        return interacting;
    }

}
