using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response : MonoBehaviour
{
    public int pathID;


    public void OnClick()
    {
        DialogueManager.DM.AdvancePath(pathID);
    }
}
