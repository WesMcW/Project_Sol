using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Player Input")]
    public KeyCode inventoryKey, quickSwitchKey;
    [Header("Cursor Inventory Object")]
    public CursorInventory CI;
    [HideInInspector]
    public static Inventory inventory;
    [Header("Inventory UI")]
    public GameObject inventoryMenu;
    public Slot[] slots;
    public EquipmentSlot[] equipSlots;


    private void Awake()
    {
        if (inventory != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            inventory = this;
        }
     
    }

    private void Start()
    {
       //For some reason the inventory works like this 
        inventoryMenu.SetActive(false);
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
        }
    }




    /// <summary>
    /// Adds item(s) to the Inventory list. Use CanAddItems() to check and see if the inventory can add the correct amount of items.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    public void AddItemToInventory(int id, int amount)
    {
        ItemInfo theItem = ItemIDManager.instance.GetItem(id).GetComponent<ItemInfo>();
        int amountLimit = theItem.amountLimit;
        bool foundLikeItem = false;
        //print("Adding itemID: " + id + " | Amount: " + amount);
        //Adds individual items for the amount there are.
        for (int i = 0; i < slots.Length; i++)
        {

            //If item pickup has more than 1... oh no...
            if (slots[i].itemID == id && (slots[i].GetAmount() + amount) < amountLimit + 1 && slots[i].GetAmount() != amountLimit)
            {
                slots[i].IncreaseAmount(amount);
                foundLikeItem = true;
               // print("Found Like Item");
                break;
            }
           
           
        }

        //If the loop did NOT found a item in the inventory that matches the itemID
        if (!foundLikeItem)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].empty)
                {
                    slots[i].AddItem(id, amount);
                    break;
                }
            }
           
        }
    }
    /// <summary>
    /// Adds indicated item to the appropriate equipment slot.
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="amount"></param>
    public void AddItemToEquipment(int itemID, int amount)
    {
        char categoryID = '0';
        categoryID = itemID.ToString()[0];

        for (int i = 0; i < equipSlots.Length; i++)
        {
            //If the equipment slot required ID matches the itemID category.
            if(equipSlots[i].requiredID == categoryID)
            {
                if (!equipSlots[i].empty)
                {
                    //If the slot is not empty, swap
                    AddItemToInventory(equipSlots[i].itemID, equipSlots[i].GetAmount());
                }
                //Swap with this
                equipSlots[i].AddItem(itemID, amount);
                break;
                //Else if the slots are all full
            }
            
            else if(equipSlots[i].requiredID != categoryID && i == equipSlots.Length - 1)
            {
                //No open slots or the item cannot be equipped
               AddItemToInventory(itemID, amount);
            }

            
        }
    }

    /// <summary>
    ///  returns the first empty slot in the inventory. NOT EQUIPMENT INVENTORY
    /// </summary>
    public Slot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].empty)
            {
                return slots[i];
            } 
        }
        return null;
    }
    /// <summary>
    /// Checks the itemID to see if it can be quick equipped.
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public bool CategoryIDCheck(int itemID)
    {
        char categoryID = '0';
        categoryID = itemID.ToString()[0];

        for (int i = 0; i < equipSlots.Length; i++)
        {
            if(categoryID == equipSlots[i].requiredID)
            {
                return true;
                
            }
        }
        return false;
    }

    /// <summary>
    /// Checks to see if the inventory can add the indicated item and amount
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CanAddItem(int itemID, int amount)
    {
        ItemInfo theItem = ItemIDManager.instance.GetItem(itemID).GetComponent<ItemInfo>();
        int amountLimit = theItem.amountLimit;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].empty)
            {
               // print("Can add to inventory");
                return true;
            } else if(slots[i].itemID == itemID && (slots[i].GetAmount() + amount) < amountLimit + 1 && slots[i].GetAmount() != amountLimit)
            {
               // print("Can add to inventory");
                return true;
               
            }
        }
        return false;
    }

    /// <summary>
    /// Checks to make sure inventory contains the correct amount of items
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool HasItems(int itemID, int amount)
    {
        int totalAmountFound = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].itemID == itemID)
            {
                //Found like item Check amount
                //Apply amount found to totalAmountFound
                totalAmountFound += slots[i].GetAmount();
            }
            if(totalAmountFound >= amount)
            {
                //Found enough like items
                return true;
            }
        }

        //Didn't find enough like items
        return false;
    }

    /// <summary>
    /// Goes through Inventory and removes the requested items and their amounts. BE SURE TO CHECK BEFOREHAND WITH HasItems() TO MAKE SURE IT'S POSSIBLE
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="amountNeeded"></param>
    public void RemoveItems(int itemID, int amountNeeded)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            print(amountNeeded);
            if(slots[i].itemID == itemID)
            {
                if(amountNeeded == 0)
                {
                    //No reason to continue checking slots
                    break;
                }
                //Found like item
                if(slots[i].GetAmount() == amountNeeded)
                {
                    //Remove item because it's equal
                    slots[i].RemoveItem();
                    break;
                } else if(slots[i].GetAmount() < amountNeeded)
                {
                    //Not enough items in slot
                    amountNeeded =  amountNeeded - slots[i].GetAmount();
                    slots[i].RemoveItem();
                } else if(slots[i].GetAmount() > amountNeeded)
                {
                    //More than enough items in the slot
                    slots[i].DecreaseAmount(amountNeeded);
                    break;
                }

            }
        }
    }
   
}
