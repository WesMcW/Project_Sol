using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCutscene : Cutscene
{
    
    public override void StartCutscene()
    {
        cutsceneStarted = true;
        CutsceneManager.instance.StartCutscene(theNPCs, dialogueLines, this, 6);
    }

    public override void EndCutscene()
    {
        cutsceneStarted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !cutsceneStarted)
        {
            StartCutscene();

        }
    }
}
