using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalControls : MonoBehaviour
{
    Animator anim;
    Journal j;

    [SerializeField]
    private GameObject questingInfo, skillTree, inv;

    int pageNum = 0;

    void Start()
    {
        j = Journal.inst;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightBracket) && j.open)
        {
            if(pageNum != 2)
            {
                anim.SetTrigger("flipL");
                pageNum++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket) && j.open)
        {
            if(pageNum != 0)
            {
                anim.SetTrigger("flipR");
                pageNum--;
            }
        }
    }

    //Hides the journal UI during animation
    public void hideStuff()
    {
        questingInfo.SetActive(false);
        skillTree.SetActive(false);
        inv.SetActive(false);
    }

    //Shows the UI for current page
    public void showStuff()
    {
        if (pageNum == 0)
            questingInfo.SetActive(true);
        else if (pageNum == 1)
            skillTree.SetActive(true);
        else if (pageNum == 2)
            inv.SetActive(true);
    }

    //Bookmarks to specific pages in the journal
    public void questMark()
    {

    }
    public void skillMark()
    {

    }
    public void invMark()
    {

    }
}
