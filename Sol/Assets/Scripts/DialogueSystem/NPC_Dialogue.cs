using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    [Header("This Object should start disabled")]
    public string name;
    [SerializeField]
    Animator anim;

    public List<int> acceptableIDs = new List<int>();

    private void OnEnable()
    {
        anim.enabled = true;
       // Debug.Log();
    }

    public Animator GetAnim()
    {
        return anim;
    }

    private void OnDisable()
    {
        anim.enabled = false;
    }
}
