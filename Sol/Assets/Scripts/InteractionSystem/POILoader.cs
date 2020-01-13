using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POILoader : MonoBehaviour{

    /// <summary>
    /// This class will select one of many game objects to turn on and turn the rest off
    /// this can be used to change one version of an object to another
    /// example a bridge could have an intact state and a destroyed state on top of each other
    /// </summary>

    [SerializeField] bool saveState = false;
    [SerializeField] string saveID = "0";

    [SerializeField] int defaultState = 0;
    public GameObject[] states;

    void Start(){
        //0 is no interaction, 1 means we've meet before... havent we???
        if (saveState){
            int state = PlayerPrefs.GetInt("POI" + saveID, 0);

            Toggle(state);
        }

    }

    public void SetState(int newState) {
        Toggle(newState);
        if (saveState) {
            PlayerPrefs.SetInt("POI" + saveID, newState);
        }
    }

    void Toggle(int newState) {
        for (int i = 0; i < states.Length; i++){
            if (i == newState){
                states[i].SetActive(true);
            }else{
                states[i].SetActive(false);
            }
        }
    }

    //This *should* work
    void OnApplicationQuit(){
        PlayerPrefs.SetInt("POI" + saveID, defaultState);
    }
}
