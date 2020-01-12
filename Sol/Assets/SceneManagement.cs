using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
   
    /// <summary>
    /// Load the given scene!
    /// </summary>
    /// <param name="sceneName">the next scenes name!</param>
    public void LoadScene(string sceneName)
    {
        //save current scene
       // GameManager.instance.SaveScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneName);
    }

    public int SceneAmount()
    {
        return SceneManager.sceneCountInBuildSettings;
    }
}
