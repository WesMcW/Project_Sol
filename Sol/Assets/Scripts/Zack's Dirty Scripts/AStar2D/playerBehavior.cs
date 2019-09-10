using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{
    public static playerBehavior current;
    private void Awake()
    {
        current = this;
    }

}
