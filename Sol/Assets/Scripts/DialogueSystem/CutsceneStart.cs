using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneStart : MonoBehaviour
{

    [TextArea(2,3)]
    [SerializeField] string CutsceneText;
    [SerializeField] string name;

    void StartCutscene()
    {
        //Start Cutscene Dialogue
        DialogueManager.DM.StartCutscene(CutsceneText, name);
        this.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCutscene();
        }
    }
}
