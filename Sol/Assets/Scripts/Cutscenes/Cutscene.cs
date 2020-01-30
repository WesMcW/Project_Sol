using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cutscene : MonoBehaviour
{
    [SerializeField] [TextArea(2,3)]
    protected string[] dialogueLines;
    [SerializeField]
    protected NPC_Dialogue[] theNPCs;
    //This bool needs to be turned on and off manually atm.
    [SerializeField]
    protected bool cutsceneStarted;
    /// <summary>
    /// Called at the start of a cutscene. It must be triggered somehow
    /// </summary>
    public abstract void StartCutscene();

    /// <summary>
    /// This is called at the end of a cutscene.
    /// </summary>
    public abstract void EndCutscene();
}
