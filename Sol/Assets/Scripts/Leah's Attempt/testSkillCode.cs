using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testSkillCode : MonoBehaviour
{
    SkillsList sl;
    public Button b1, b2, b3;

    // Start is called before the first frame update
    void Start()
    {
        sl = GetComponent<SkillsList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sl.chargeAttacks[0].active) b1.GetComponent<Image>().color = Color.green;       // this if statement will enable bools in character movement/controls
        else b1.GetComponent<Image>().color = Color.red;

        if (sl.chargeAttacks[1].active) b2.GetComponent<Image>().color = Color.green;
        else b2.GetComponent<Image>().color = Color.red;

        if (sl.chargeAttacks[2].active) b3.GetComponent<Image>().color = Color.green;
        else b3.GetComponent<Image>().color = Color.red;
    }
}
