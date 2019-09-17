using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // I'm thinking this could be where all player stats (level, xp, max health, speed, damage, defense, etc) are housed?
    public int xp = 0, level = 1, xpToLevel = 2;
    public int skillPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            xp++;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            skillPoints++;
        }

        if(xp >= xpToLevel)     // Level Up!
        {
            xp = xp - xpToLevel;
            level++;
            skillPoints++;
            Debug.Log("Level Up! You are now level " + level);
            xpToLevel = (int)Mathf.Ceil(xpToLevel * 1.8F);
        }
    }
}
