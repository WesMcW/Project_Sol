using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneGame : MonoBehaviour
{
    public float currentValue = 0;
    
    [Header("Difficulty")]
    public float speed;
    public float percentDifficulty;
    private int timesComplete = 0;
    private bool flip;
    private int timesSucceeded;

    [Header("Slider Background")]
    public RectTransform sliderBackground;
    [Header("Target Zone")]
    public RectTransform targetZone;

    [Header("The Slider")]
    public Slider slider;

    private float targetLow, targetHigh;
    private bool complete;

    // Start is called before the first frame update
    void Start()
    {

        percentDifficulty = percentDifficulty / 100;
        float size = percentDifficulty * sliderBackground.sizeDelta.x;

        targetZone.sizeDelta = new Vector2(size, targetZone.sizeDelta.y);
        targetLow = 50 - ((percentDifficulty * 100) / 2);
        targetHigh = 50 + ((percentDifficulty * 100) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the currentValue
        if (!complete)
        {
            UpdateValue();
        }
       
       

        //Detects whether the slider should flip or not.
        CheckFlip();

        //Update Slider Value
        UpdateSliderUI();

        //Checks for the player to stop the bar at the right point.
        if (Input.GetKeyDown(KeyCode.Space) && timesComplete < 3)
        {
          if(currentValue >= targetLow && currentValue <= targetHigh)
            {
                timesSucceeded++;
            }
            timesComplete++;
          
        } else if(timesComplete >= 3)
        {
            //game is complete
            complete = true;
        }

    

      
    }

    /// <summary>
    /// Updates the current value based on time
    /// </summary>
    void UpdateValue()
    {
        //If the current value should be decreasing
        if (flip)
        {
            currentValue -= speed * Time.deltaTime;
        }
        //If the current value should be increasing
        else
        {
            currentValue += speed * Time.deltaTime;
        }
    }

    /// <summary>
    /// Checks to see if the value needs to flip or not
    /// </summary>
    void CheckFlip()
    {
        if (currentValue >= 100)
        {
            flip = true;
        }
        else if (currentValue <= 0)
        {
            flip = false;
        }
    }

    /// <summary>
    /// Update the slider value UI
    /// </summary>
    void UpdateSliderUI()
    {
        if(!complete)
        slider.value = currentValue;
    }

    /// <summary>
    /// Returns the result percentage
    /// </summary>
    /// <returns></returns>
    public float Result()
    {
        return (timesSucceeded / 3f);
    }

    /// <summary>
    /// Returns whether or not the game is complete
    /// </summary>
    /// <returns></returns>
    public bool isComplete()
    {
        return complete;
    }

    /// <summary>
    /// Reset the zone game for future use
    /// </summary>
    public void ResetGame()
    {
        currentValue = 0;
        timesSucceeded = 0;
        timesComplete = 0;
        complete = false;

    }

}
