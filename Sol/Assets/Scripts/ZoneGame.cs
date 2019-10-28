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

    private bool flip;

    [Header("Slider Background")]
    public RectTransform sliderBackground;
    [Header("Target Zone")]
    public RectTransform targetZone;

    [Header("The Slider")]
    public Slider slider;

    
    private float targetLow, targetHigh;

    public bool success;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
          if(currentValue >= targetLow && currentValue <= targetHigh)
            {
                success = true;
            }
          else
            {
                success = false;
            }
            complete = true;
        }

    

      
    }

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

    void UpdateSliderUI()
    {
        if(!complete)
        slider.value = currentValue;
    }


   
}
