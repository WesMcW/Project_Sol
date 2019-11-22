using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalControls : MonoBehaviour
{
    [SerializeField]
    private GameObject questingInfo;

    int pageNum;

    void Start()
    {
        
    }

    void Update()
    {
        
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
