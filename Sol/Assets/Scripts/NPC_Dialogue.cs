using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public string name;
    [SerializeField]
    Animator anim;
    private void OnEnable()
    {
        anim.enabled = true;
       // Debug.Log();
    }

    public Animator GetAnim()
    {
        return anim;
    }
}
