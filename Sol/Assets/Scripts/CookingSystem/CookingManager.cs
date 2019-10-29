using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public static CookingManager CM;

    private void Awake()
    {
        if(CM == null)
        {
            CM = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
