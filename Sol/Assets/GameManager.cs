using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Slot[] savedSlots;
    private EquipmentSlot[] savedEquipSlots;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
