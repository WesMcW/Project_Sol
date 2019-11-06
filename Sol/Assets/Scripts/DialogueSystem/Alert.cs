using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    SpriteRenderer sr;
    private void OnEnable()
    {
        NPC_Dialogue npc = DialogueManager.DM.GetCurrentNPC();
        if (npc.transform.GetComponent<SpriteRenderer>())
        {
            sr = npc.transform.GetComponent<SpriteRenderer>();
        }
            
        else
        {
            sr = npc.transform.GetComponentInParent<SpriteRenderer>();
        }
           
        this.transform.position = npc.transform.position + new Vector3(0, sr.bounds.size.y * 1.2f);
    }
}
