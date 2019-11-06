using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    
    private void OnEnable()
    {
        NPC_Dialogue npc = DialogueManager.DM.GetCurrentNPC();
        SpriteRenderer sr = npc.transform.GetComponent<SpriteRenderer>() ?? npc.transform.parent.GetComponent<SpriteRenderer>();
        this.transform.position = npc.transform.position + new Vector3(0, sr.bounds.size.y);
    }
}
