using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Response : MonoBehaviour
{
    public int pathID;
    public bool isCutscene;


    public void OnClick()
    {
        if (!isCutscene)
        {
            DialogueManager.DM.AdvancePath(pathID);
        }else
        {
            CutsceneManager.instance.AdvancePath();
        }
       
    }
}
