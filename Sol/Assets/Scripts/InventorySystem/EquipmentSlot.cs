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
    private KeyCode switchKey;
    [Header("Required Category ID")]
    public char requiredID;
    [Header("Default UI Image")]
    public Sprite defaultImage;
    private Image img;
    private Inventory inventoryManager;
    private int amount;
    // Start is called before the first frame update

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    private void Start()
    {
        inventoryManager = Inventory.inventory;
        switchKey = inventoryManager.quickSwitchKey;
    }
    public void AddItem(int id, int theAmount)
    {
        //if the item being fed is null. Default to blank
        if (id == 0)
        {
            itemID = 0;
            empty = true;
            img.sprite = defaultImage;
            amount = 0;
        }
        else
        {
            GameObject theItem = ItemIDManager.instance.GetItem(id);   
            img.sprite = theItem.GetComponent<ItemInfo>().sprite;
            empty = false;
            amount = theAmount;
        }
        itemID = id;  
    }

    public void RemoveItem()
    {
        img.sprite = defaultImage;
        itemID = 0;
        empty = true;
        amount = 0;
    }

    public int GetAmount()
    {
        return amount;
    }
    public void IncreaseAmount(int amt)
    {
        amount += amt;
      //  amountText.text = amount.ToString();
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
            if (Input.GetKey(switchKey) && itemID > 0 && inventoryManager.CanAddItem(itemID, amount))
            {
                inventoryManager.AddItemToInventory(itemID, amount);
                RemoveItem();
                AddItem(theItemID, itemAmount);
                inventoryManager.CI.RemoveItem();
            }
            //If there isnt an item in the cursor but an item here.
        } else if (Input.GetKey(switchKey) && itemID > 0 && inventoryManager.CanAddItem(itemID, amount))
        {
            //Send the item here into an empty slot.
            inventoryManager.AddItemToInventory(itemID, amount);
            RemoveItem();
        }
        //if the player isn't holding down the quick switch key.
        if (!Input.GetKey(switchKey))
        {
            //If there is an item being held by the cursor and this slot is empty, add the item.
            if (categoryID == requiredID && itemID == 0)
            {
                AddItem(theItemID, itemAmount);
                inventoryManager.CI.RemoveItem();
            }
            //If the items are different, swap.
            else if (theItemID != itemID && categoryID == requiredID)
            {
                inventoryManager.CI.AddItem(itemID, amount);
                AddItem(theItemID, itemAmount);
            }
            //If the cursor has nothing in it and this slot holds an item
            else if (theItemID == 0 && itemID > 0)
            {

                inventoryManager.CI.AddItem(itemID, amount);
                RemoveItem();
            }
        }

      
    }


}
