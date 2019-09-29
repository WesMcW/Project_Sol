using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    [Header("Speed of Text")]
    [SerializeField]
    float timeDelay;

    [Header("Main Dialogue Object")]
    [SerializeField]
    GameObject DialogueObj;

    [Header("Player Input")]
    [SerializeField]
    KeyCode dialogueInitiateKey;

    private string currentText;
    private NPC_Dialogue npcDiag = null;

    [Header("UI Text Objects")]
    [SerializeField]
    TextMeshProUGUI npcName;

    [SerializeField]
    TextMeshProUGUI npcText;

    [SerializeField]
    TextMeshProUGUI response1;

    [SerializeField]
    TextMeshProUGUI response2;

    [SerializeField]
    TextMeshProUGUI response3;

    public static DialogueManager DM;

    //[SerializeField]
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
            npcDiag.enabled = true;
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
        npcName.text = npcDiag.name;
        //Animate the Text printing
        StartCoroutine(printText(text));
    }

    //Prints out the text sequentially
    IEnumerator printText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            currentText = text.Substring(0, i + 1);
            npcText.text = currentText;
            yield return new WaitForSeconds(timeDelay);
        }
        //IDK if this is needed or not
        StopCoroutine(printText(text));
    }


    public NPC_Dialogue GetCurrentNPC()
    {
        return npcDiag;
    }

    /// <summary>
    /// Advances the Finite State Machine to the next path which matches the correct ID
    /// </summary>
    /// <param name="id"></param>
    public void AdvancePath(int id)
    {
        //Stop all the coroutines because the text gets glitchy if you dont.
        StopAllCoroutines();
        npcDiag.GetAnim().SetTrigger("path" + id);
    }

    public void FoundNPC(NPC_Dialogue Npc)
    {
        npcDiag = Npc;
        canTalk = true;
    }

    public void RemoveNPC()
    {
        npcDiag = null;
        canTalk = false;
    }

    /// <summary>
    /// Ends the current conversation and resets all of the objects to their original states.
    /// </summary>
    public void EndConversation()
    {
        //Stop the text from printing and glitching out
        StopAllCoroutines();
        //Reset the conversation back to the beginning
        npcDiag.GetAnim().SetTrigger("Back");
        //Turn off Dialogue Object
        DialogueObj.SetActive(false);
        //No longer talking
        talking = false;

        npcDiag.enabled = false;

    }
}
