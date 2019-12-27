using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWander : MonoBehaviour
{
    Vector2 dirToGo = new Vector2(0,0);
    float maxTimer = 5, timer = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector2 FindDirection()
    {
        Vector3 dir = Random.onUnitSphere;
        return new Vector2(dir.x, dir.y);
    }
}
