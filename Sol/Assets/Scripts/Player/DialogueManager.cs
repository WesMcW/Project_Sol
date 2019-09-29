using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject DialogueObj;
    [SerializeField]
    KeyCode dialogueInitiateKey;

    private NPC_Dialogue theNPC = null;
    [SerializeField]
    TextMeshProUGUI npcName, npcText, response1, response2, response3;
    public static DialogueManager DM;

    [SerializeField]
    bool canTalk, talking;
    // Start is called before the first frame update
    private void Awake()
    {
        if(DM == null)
        {
            DM = this;
        } else
        {
            Destroy(this);
        }
       
    }

  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dialogueInitiateKey) && canTalk && !talking)
        {
            DialogueObj.SetActive(true);
            theNPC.enabled = true;
            talking = true;
        } else if (talking && Input.GetKeyDown(dialogueInitiateKey))
        {
            EndConversation();
        }
    }

    /// <summary>
    /// Initiates Dialogue. Requires NPC text but the responses are optional.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="res_1"></param>
    /// <param name="res_2"></param>
    /// <param name="res_3"></param>
    public void StartDialogue(string text, string res_1 = null, string res_2 = null, string res_3 = null)
    {
        if(res_1 == null)
        {
            response1.gameObject.SetActive(false);
        }
        else
        {
            response1.gameObject.SetActive(true);
            response1.text = res_1;
        }
        if(res_2 == null)
        {
            response2.gameObject.SetActive(false);
        } else
        {
            response2.gameObject.SetActive(true);
            response2.text = res_2;
        }
        if(res_3 == null)
        {
            response3.gameObject.SetActive(false);
        }
        else
        {
            response3.gameObject.SetActive(true);
            response3.text = res_3;
        }
        npcName.text = theNPC.name;
        npcText.text = text;
    }

    public NPC_Dialogue GetCurrentNPC()
    {
        return theNPC;
    }

    public void AdvancePath(int id)
    {
        theNPC.GetAnim().SetTrigger("path" + id);
    }

    public void FoundNPC(NPC_Dialogue Npc)
    {
        theNPC = Npc;
        canTalk = true;
    }

    public void RemoveNPC()
    {
        theNPC = null;
        canTalk = false;
    }

    public void EndConversation()
    {
        //Could be changed
        theNPC.GetAnim().SetTrigger("Back");
        DialogueObj.SetActive(false);
        talking = false;
       

    }
}
