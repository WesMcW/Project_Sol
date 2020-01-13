using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PPDelete : EditorWindow
{
    [MenuItem("Tools/Rest Player Pref")]
    public static void ResetPlayerPref() {
        PlayerPrefs.DeleteAll();
        Debug.Log("PP DELETED");
    }
}
