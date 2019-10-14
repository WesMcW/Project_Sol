using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    
    private void OnEnable()
    {
        NPC_Dialogue npc = DialogueManager.DM.GetCurrentNPC();
        this.transform.position = npc.transform.position + new Vector3(0, npc.transform.parent.GetComponent<SpriteRenderer>().bounds.size.y);
    }
}
