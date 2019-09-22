using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Player Input")]
    public KeyCode inventoryKey;
    [Header("Cursor Inventory Object")]
    public CursorInventory CI;
    [HideInInspector]
    public static Inventory inventory;
    [Header("Inventory UI")]
    public GameObject inventoryMenu;
    public Slot[] slots;

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




    //Adds item to inventory list.
    public void AddItemToInventory(int id, int amount)
    {
        ItemInfo theItem = ItemIDManager.instance.GetItem(id).GetComponent<ItemInfo>();
        int amountLimit = theItem.amountLimit;
        bool foundLikeItem = false;

        //Adds individual items for the amount there are.
        for (int i = 0; i < slots.Length; i++)
        {

            //If item pickup has more than 1... oh no...
            if (slots[i].itemID == id && slots[i].GetAmount() + amount < amountLimit + 1)
            {
                slots[i].IncreaseAmount(amount);
                foundLikeItem = true;
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
   
}
