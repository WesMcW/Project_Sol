using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Slot[] savedSlots;
    private EquipmentSlot[] savedEquipSlots;
   // public SavedScene[] savedScenes;
    /*
     * The Game Manager's goal is to keep track of the players progress.
     * This includes saving between scenes, keeping track of quest progression etc.
     * 
     */

    public static GameManager instance;
    private void Awake()
    {
        //Singleton
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else
        {
            Destroy(this.gameObject);
        }
       
    }

    /*
    private void Start()
    {
        savedScenes = new SavedScene[SceneManagement.instance.SceneAmount()];
    }
    public void SaveScene(int sceneIndex)
    {
        bool running = true;
        int i = 0;
        
        print(scene);
        savedScenes[sceneIndex] = scene;
        //savedScenes[sceneIndex] = new SavedScene();
        //Save the scene
        do
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                running = false;
            }
            if(GameObject.FindObjectOfType<SaveObject>() != null)
            {
                GameObject obj = GameObject.FindObjectOfType<SaveObject>().gameObject;
                savedScenes[sceneIndex].savedItems.Add(obj);
                print("Saved: " + obj.name);
                Destroy(obj);
                i++;
            } else
            {
                //finished
                running = false;
                print("save complete");
            }
           
        } while (running);
    }
    */

    /// <summary>
    ///  Save the current inventory
    /// </summary>
    /// <param name="theSlots">Normal Slots</param>
    /// <param name="equipSlots">Equipment Slots</param>
    public void SaveInventory(Slot[] theSlots, EquipmentSlot[] equipSlots)
    {
        //Save the normal slots
        for (int i = 0; i < theSlots.Length; i++)
        {
            //Save the slot
            savedSlots[i] = theSlots[i];
        }

        //Save the equipment slots
        for (int i = 0; i < equipSlots.Length; i++)
        {
            //Save the slot
            savedEquipSlots[i] = equipSlots[i];
        }
    }

    /// <summary>
    /// Returns the saved normal slots
    /// </summary>
    /// <returns></returns>
    public Slot[] LoadNormalSlots()
    {
        return savedSlots;
    }

    /// <summary>
    /// Returns the saved Equipment Slots
    /// </summary>
    /// <returns></returns>
    public EquipmentSlot[] LoadEquipmentSlots()
    {
        return savedEquipSlots;
    }

   
}

public class SavedScene : MonoBehaviour
{
    public List<GameObject> savedItems;

    public SavedScene()
    {
        savedItems = new List<GameObject>();
    }
}
