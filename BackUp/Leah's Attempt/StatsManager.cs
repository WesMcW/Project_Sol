using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // I'm thinking this could be where all player stats (level, xp, max health, speed, damage, defense, etc) are housed/ easily viewed together

    public GameObject player;
    public int xp = 0, level = 1, xpToLevel = 2;
    public int skillPoints = 0, totalSP = 0;
    public float speedRef, rollSpeedRef;    // in Player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        speedRef = player.GetComponent<Player>().moveSpeed;
        rollSpeedRef = player.GetComponent<Player>().rollSpeed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            xp++;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            skillPoints++;
            totalSP++;
        }

        if(xp >= xpToLevel)     // Level Up!
        {
            xp = xp - xpToLevel;
            level++;
            skillPoints++;
            totalSP++;
            Debug.Log("Level Up! You are now level " + level);
            xpToLevel = (int)Mathf.Ceil(xpToLevel * 1.8F);
        }
    }

    public void updateStats()   // better than in update maybe
    {
        player.GetComponent<Player>().moveSpeed = speedRef;
        player.GetComponent<Player>().rollSpeed = rollSpeedRef;
    }
}