using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [Header("Speed of Text")]
    [SerializeField]
    float timeDelay = 0.25f;
    [SerializeField]
    TextMeshProUGUI npcNameUI, npcText;
    [SerializeField]
    GameObject dialogueObj;
    [SerializeField]
    TextMeshProUGUI response1;
    [SerializeField]
    GameObject response2, response3;
    private string currentText;
    string[] dialogueLines;
    NPC_Dialogue[] currentNPCs;
    int currentIndex;
    Cutscene theCurrentCutscene;
    [SerializeField]
    Camera mainCamera;
    float standardOrtho;
    [SerializeField]
    float zoomSpeed = 2;
    public static CutsceneManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
        standardOrtho = mainCamera.orthographicSize;
    }

   /// <summary>
   /// Starts a cutscene with the given lines of text and the given NPC's. 
   /// </summary>
   /// <param name="theNPCs">The NPC's used</param>
   /// <param name="theLines">The lines of dialogue to play</param>
   /// <param name="theCutscene">The Current Cutscene playing</param>
    public void StartCutscene(NPC_Dialogue[] theNPCs, string[] theLines, Cutscene theCutscene)
    {
        StopAllCoroutines();
        theCurrentCutscene = theCutscene;
        currentIndex = 0;
        dialogueLines = theLines;
        currentNPCs = theNPCs;
        npcNameUI.text = theNPCs[0].name;
        dialogueObj.SetActive(true);
        response1.gameObject.SetActive(true);
        response2.SetActive(false);
        response3.SetActive(false);
        response1.rectTransform.parent.gameObject.GetComponent<Response>().isCutscene = true;
        response1.text = "Continue";
        Camera.main.GetComponent<CameraController>().enabled = false;
        Player.instance.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(printText(dialogueLines[currentIndex]));
        StartCoroutine(CameraMove(currentNPCs[0].transform.position));
    }

    /// <summary>
    /// Starts a cutscene with the given lines of text and the given NPC's. The camera will zoom in on the NPC as well.
    /// </summary>
    /// <param name="theNPCs"></param>
    /// <param name="theLines"></param>
    /// <param name="theCutscene"></param>
    /// <param name="targetOrtho"></param>
    public void StartCutscene(NPC_Dialogue[] theNPCs, string[] theLines, Cutscene theCutscene, float targetOrtho)
    {
        StopAllCoroutines();
        theCurrentCutscene = theCutscene;
        currentIndex = 0;
        dialogueLines = theLines;
        currentNPCs = theNPCs;
        npcNameUI.text = theNPCs[0].name;
        dialogueObj.SetActive(true);
        response1.gameObject.SetActive(true);
        response2.SetActive(false);
        response3.SetActive(false);
        response1.rectTransform.parent.gameObject.GetComponent<Response>().isCutscene = true;
        response1.text = "Continue";
        Camera.main.GetComponent<CameraController>().enabled = false;
        Player.instance.GetComponent<PlayerController>().enabled = false;
        StartCoroutine(CameraZoom(targetOrtho));
        StartCoroutine(CameraMove(currentNPCs[0].transform.position));
        StartCoroutine(printText(dialogueLines[currentIndex]));
    }


    IEnumerator CameraZoom(float targetOrtho)
    {
        while(mainCamera.orthographicSize != targetOrtho)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetOrtho, zoomSpeed * Time.deltaTime);
           
            yield return null;
        }
       
    }

    IEnumerator CameraMove(Vector3 targetLocation)
    {
       // print(Vector3.Distance(mainCamera.transform.position, targetLocation));
       // bool running = true;
        while (Vector3.Distance(mainCamera.transform.position, targetLocation) > 10)
        {
           // print(Vector3.Distance(mainCamera.transform.position, targetLocation));
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetLocation, zoomSpeed * Time.deltaTime);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -10);
            
            yield return null;
        }
        yield return null;

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
       // StopCoroutine(printText(text));
    }

    /// <summary>
    /// Advance to the next line of dialogue
    /// </summary>
    public void AdvancePath()
    {
        StopAllCoroutines();
        currentIndex++;
        if(currentIndex > dialogueLines.Length - 1)
        {
            //End of cutscene
            EndCutscene();
        } else
        {
            StartCoroutine(printText(dialogueLines[currentIndex]));
            //If the index is less than the current amount of npcs then change the name. otherwise keep the current npc name.
            if(currentIndex < currentNPCs.Length)
            {
                npcNameUI.text = currentNPCs[currentIndex].name;
                StartCoroutine(CameraMove(currentNPCs[currentIndex].transform.position));
            }
           
        }
        
    }

    /// <summary>
    /// End the current cutscene
    /// </summary>
    void EndCutscene()
    {
        StopAllCoroutines();
        response1.gameObject.SetActive(false);
        response1.rectTransform.parent.gameObject.GetComponent<Response>().isCutscene = false;
        Camera.main.GetComponent<CameraController>().enabled = true;
        Player.instance.GetComponent<PlayerController>().enabled = true;
        dialogueObj.SetActive(false);
        theCurrentCutscene.EndCutscene();
    }

}
