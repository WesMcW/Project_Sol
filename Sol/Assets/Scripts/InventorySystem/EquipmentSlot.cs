using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EquipmentSlot : MonoBehaviour
{
    public bool empty;
    [HideInInspector]
    public int itemID;

    [Header("Required Category ID")]
    public char requiredID;
    [Header("Default UI Image")]
    public Sprite defaultImage;
    private Image img;
    private Inventory inventoryManager;
    // Start is called before the first frame update

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        inventoryManager = Inventory.inventory;
    }
    public void AddItem(int id, int theAmount)
    {
        //if the item being fed is null. Default to blank
        if (id == 0)
        {
            itemID = 0;
            empty = true;
            img.sprite = defaultImage;
        }
        else
        {
            GameObject theItem = ItemIDManager.instance.GetItem(id);   
            img.sprite = theItem.GetComponent<ItemInfo>().sprite;
            empty = false;
        }
        itemID = id;  
    }

    public void RemoveItem()
    {
        img.sprite = defaultImage;
        itemID = 0;
        empty = true;
    }

    public void OnClick()
    {

        //Declare the correct variables.
        GameObject theItem;
        //These MUST be declared as 0. 
        int theItemID = 0;
        int itemAmount = 0;
        char categoryID = '0';
        //Checks if there is an item in your curser
        if (inventoryManager.CI.GetCurrentItem() != 0)
        {
          
            theItem = ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem());
            theItemID = theItem.GetComponent<ItemInfo>().ItemID;
            itemAmount = inventoryManager.CI.GetCurrentAmount();
            categoryID = theItemID.ToString()[0];
        }

        //If there is an item being held by the cursor and this slot is empty, add the item.
        if (categoryID == requiredID && itemID == 0)
        {
            AddItem(theItemID, itemAmount);
            inventoryManager.CI.RemoveItem();
        }
        //If the items are different, swap.
        else if (theItemID != itemID && categoryID == requiredID)
        {
            inventoryManager.CI.AddItem(itemID, 1);
            AddItem(theItemID, itemAmount);
        }
        //If the cursor has nothing in it and this slot holds an item
        else if (theItemID == 0 && itemID > 0)
        {

            inventoryManager.CI.AddItem(itemID, 1);
            RemoveItem();
        }
    }


}
