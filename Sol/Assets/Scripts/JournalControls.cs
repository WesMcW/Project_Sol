using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalControls : MonoBehaviour
{
    Animator anim;
    Journal j;

    [SerializeField]
    private GameObject questingInfo, skillTree;

    int pageNum;

    void Start()
    {
        j = Journal.inst;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftBracket) && j.open)
        {
            anim.SetTrigger("flipL");

        }
        else if (Input.GetKeyDown(KeyCode.RightBracket) && j.open)
        {
            anim.SetTrigger("flipR");
        }
    }

    public void hideQuesting()
    {
        questingInfo.SetActive(false);
    }

    public void showQuesting()
    {
        questingInfo.SetActive(true);
    }
}
