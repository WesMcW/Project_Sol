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
            //anim.SetTrigger("flipL");
            if(pageNum != 2)
            {
                anim.SetTrigger("flipL");
                pageNum++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket) && j.open)
        {
            //anim.SetTrigger("flipR");
            if(pageNum != 0)
            {
                anim.SetTrigger("flipR");
                pageNum--;
            }
        }
    }

    public void hideStuff()
    {
        questingInfo.SetActive(false);
        skillTree.SetActive(false);
        inv.SetActive(false);
    }

    public void showStuff()
    {
        if (pageNum == 0)
            questingInfo.SetActive(true);
        else if (pageNum == 1)
            skillTree.SetActive(true);
        else if (pageNum == 2)
            inv.SetActive(true);
    }
}
