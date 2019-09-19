using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testSkillCode : MonoBehaviour
{
    // All this does is change the color of the buttons for enabled/active

    SkillsList sl;
    public Button b1, b2, b3;

    void Start()
    {
        sl = GetComponent<SkillsList>();
    }

    void Update()
    {
        if (sl.chargeAttacks[0].active) b1.GetComponent<Image>().color = Color.green;
        else if (sl.chargeAttacks[0].enabled && !sl.chargeAttacks[0].active) b1.GetComponent<Image>().color = Color.yellow;
        else if (!sl.chargeAttacks[0].enabled && sl.chargeAttacks[0].cost <= sl.spRef) b1.GetComponent<Image>().color = Color.red;
        else b1.GetComponent<Image>().color = Color.magenta;

        if (sl.chargeAttacks[1].active) b2.GetComponent<Image>().color = Color.green;
        else if (sl.chargeAttacks[1].enabled && !sl.chargeAttacks[1].active) b2.GetComponent<Image>().color = Color.yellow;
        else if (!sl.chargeAttacks[1].enabled && sl.chargeAttacks[1].cost <= sl.spRef) b2.GetComponent<Image>().color = Color.red;
        else b2.GetComponent<Image>().color = Color.magenta;

        if (sl.chargeAttacks[2].active) b3.GetComponent<Image>().color = Color.green;
        else if (sl.chargeAttacks[2].enabled && !sl.chargeAttacks[2].active) b3.GetComponent<Image>().color = Color.yellow;
        else if (!sl.chargeAttacks[2].enabled && sl.chargeAttacks[2].cost <= sl.spRef) b3.GetComponent<Image>().color = Color.red;
        else b3.GetComponent<Image>().color = Color.magenta;
    }
}
