using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static GameObject theObj;

    private void Awake()
    {
        if(theObj == null)
        {
            theObj = this.gameObject;
            DontDestroyOnLoad(theObj);
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
