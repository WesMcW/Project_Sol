using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingGameChecker : MonoBehaviour
{
    ZoneGame theGame;

    /*
     * This script only runs when on an enabled object. It checks every frame to see if the cooking game is complete.
     *  if it is complete, then tell the manager it is done.
     */
    private void Awake()
    {
        theGame = GetComponent<ZoneGame>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (theGame.isComplete())
        {
            CookingManager.CM.GameFinished();
            this.gameObject.SetActive(false);
        }
    }
}
