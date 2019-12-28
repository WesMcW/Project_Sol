using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLSaver : MonoBehaviour
{
    public static XMLSaver instance;

    // Start is called before the first frame update
    void Awake()
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
    public void SaveItems()
    {
        //open Xml file
        XmlSerializer serializer = new XmlSerializer(typeof(SettingSaver));

    }


}
[System.Serializable]
public class SettingSaver
{
    public List<Settings> settingType = new List<Settings>();

}
[System.Serializable]
public class Settings
{
    //Settings of the specific player in question
    //for multiple saves on one game.
    int settingsID;
    float masterSound, musicSound, effectsSound;
    bool controller, fullscreen;
    /*
     * Constructs the settings object to be saved
     */
    public Settings(int settingsID,float masterSound, float musicSound, float effectsSound, bool controller, bool fullscreen)
    {
        this.masterSound = masterSound;
        this.musicSound = musicSound;
        this.effectsSound = effectsSound;
        this.controller = controller;
        this.fullscreen = fullscreen;
        this.settingsID = settingsID;
    }
    public int GetSettingsID()
    {
        return settingsID;
    }
    public float GetMasterSound()
    {
        return masterSound;
    }
    public float GetMusicSound()
    {
        return musicSound;
    }
    public float GetEffectsSound()
    {
        return effectsSound;
    }
    public bool GetController()
    {
        return controller;
    }
    public bool GetFullscreen()
    {
        return fullscreen;
    }
    public void SetMasterSound(float n)
    {
        masterSound = n;
    }
    public void SetMusicSound(float n)
    {
        musicSound = n;
    }
    public void SetEffectsSound(float n)
    {
        effectsSound = n;
    }
    public void SetController(bool n)
    {
        controller = n;
    }
    public void SetFullscreen(bool n)
    {
        fullscreen = n;
    }
}