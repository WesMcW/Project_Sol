using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            InteractionManager.IM.SetTarget(collision.transform);
        }
        if (collision.CompareTag("NPC"))
        {
            DialogueManager.DM.RemoveNPC();
            DialogueManager.DM.FoundNPC(collision.GetComponent<NPC_Dialogue>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            InteractionManager.IM.SetTarget(collision.transform);
        }
        if (collision.CompareTag("NPC") && DialogueManager.DM.GetCurrentNPC() != collision.GetComponent<NPC_Dialogue>())
        {
            DialogueManager.DM.RemoveNPC();
            DialogueManager.DM.FoundNPC(collision.GetComponent<NPC_Dialogue>());
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            InteractionManager.IM.SetTarget(null);
        }
        //if its the correct npc
        if (collision.CompareTag("NPC") && (DialogueManager.DM.GetCurrentNPC() == collision.GetComponent<NPC_Dialogue>()))
        {
            DialogueManager.DM.RemoveNPC();
        }
    }
}
