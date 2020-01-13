using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class CandleFlicker : MonoBehaviour
{
    private UnityEngine.Experimental.Rendering.Universal.Light2D light;
    public float speed;
    public Vector2 boundaries;
    bool flip;
    private void Awake()
    {
        light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }
   
    // Update is called once per frame
    void Update()
    {
        CheckFlip();
        UpdateValue();
        
    }

    void UpdateValue()
    {
        //If the current value should be decreasing
        if (flip)
        {
            light.intensity -= speed * Time.deltaTime;
        }
        //If the current value should be increasing
        else
        {
            light.intensity += speed * Time.deltaTime;
        }
    }
    void CheckFlip()
    {
        if (light.intensity >= boundaries.y)
        {
            flip = true;
        }
        else if (light.intensity <= boundaries.x)
        {
            flip = false;
        }
    }

}
