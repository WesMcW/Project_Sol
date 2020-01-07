using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipmentSlot : MonoBehaviour, IBeginDragHandler, IDropHandler, IPointerClickHandler, IPointerDownHandler
{
    public bool empty;
    [HideInInspector]
    public int itemID;
    private KeyCode switchKey, singleItemKey;
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
        singleItemKey = inventoryManager.singleItemKey;
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

    public void DecreaseAmount(int amt)
    {
        amount -= amt;
        if(amount <= 0)
        {
            RemoveItem();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject && eventData.button == PointerEventData.InputButton.Left)
        {
            //Pick up an item
            //Only if the slot is not empty, pick up the item in here.
            if (itemID != 0)
            {
                inventoryManager.CI.AddItem(itemID, amount);
                RemoveItem();
               // print("Picked up: " + inventoryManager.CI.GetCurrentItem());
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        // Debug.Log("Mouse Up: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject && eventData.button == PointerEventData.InputButton.Left)
        {
            if (CheckCategory(inventoryManager.CI.GetCurrentItem()))
            {
                int amountLimit = ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem()).GetComponent<ItemInfo>().amountLimit;
                //Place an Item
                //Check if the slot is currently empty or not
                if (itemID == 0)
                {
                    //Slot is empty
                    AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                }
                else if (itemID == inventoryManager.CI.GetCurrentItem())
                {
                    //They're the samething

                    //Check to make sure the amount limit isnt reached if the item is added
                    if (inventoryManager.CI.GetCurrentAmount() + amount <= amountLimit)
                    {
                        //Add the items
                        IncreaseAmount(inventoryManager.CI.GetCurrentAmount());
                        inventoryManager.CI.RemoveItem();
                    }
                    else if (inventoryManager.CI.GetCurrentAmount() + amount > amountLimit)
                    {
                        //Add what you can
                        int amt = amountLimit - amount;
                        if (amt == 0)
                        {
                            //Then swap the items
                            int id = itemID;
                            int amt2 = amount;
                            AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                            inventoryManager.CI.AddItem(id, amt2);
                        }
                        else
                        {
                            inventoryManager.CI.DecreaseCurrentAmount(amt);
                            IncreaseAmount(amt);
                        }
                    }

                }
                else if (itemID != inventoryManager.CI.GetCurrentItem())
                {
                    //Theyre different
                    //Put the dropped item into the slot and put the current new item into the cursor inventory
                    int id = itemID;
                    int amt = amount;
                    AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.AddItem(id, amt);
                }
                else
                {
                    //Swap 
                    inventoryManager.AddItemToInventory(itemID, amount);
                    AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                }
            }
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject) && inventoryManager.CI.GetCurrentItem() != 0 && eventData.button == PointerEventData.InputButton.Left)
        {
            if (CheckCategory(inventoryManager.CI.GetCurrentItem()))
            {
                int amountLimit = ItemIDManager.instance.GetItem(inventoryManager.CI.GetCurrentItem()).GetComponent<ItemInfo>().amountLimit;
                //Place an Item
                //Check if the slot is currently empty or not
                if (itemID == 0)
                {
                    //Slot is empty
                    AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                }
                else if (itemID == inventoryManager.CI.GetCurrentItem())
                {
                    //They're the samething

                    //Check to make sure the amount limit isnt reached if the item is added
                    if (inventoryManager.CI.GetCurrentAmount() + amount <= amountLimit)
                    {
                        //Add the items
                        IncreaseAmount(inventoryManager.CI.GetCurrentAmount());
                        inventoryManager.CI.RemoveItem();
                    }
                    else if (inventoryManager.CI.GetCurrentAmount() + amount > amountLimit)
                    {
                        //Add what you can
                        int amt = amountLimit - amount;
                        if (amt == 0)
                        {
                            //Then swap the items
                            int id = itemID;
                            int amt2 = amount;
                            AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                            inventoryManager.CI.AddItem(id, amt2);
                        }
                        else
                        {
                            inventoryManager.CI.DecreaseCurrentAmount(amt);
                            IncreaseAmount(amt);
                        }



                    }

                }
                else if (itemID != inventoryManager.CI.GetCurrentItem())
                {
                    //Theyre different
                    //Put the dropped item into the slot and put the current new item into the cursor inventory
                    int id = itemID;
                    int amt = amount;
                    AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.AddItem(id, amt);
                }
                else
                {
                    //Swap 
                    inventoryManager.AddItemToInventory(itemID, amount);
                    AddItem(inventoryManager.CI.GetCurrentItem(), inventoryManager.CI.GetCurrentAmount());
                    inventoryManager.CI.RemoveItem();
                }
            }
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if ((eventData.pointerCurrentRaycast.gameObject == this.gameObject || eventData.pointerCurrentRaycast.gameObject == this.transform.GetChild(0).gameObject))
        {
            if (Input.GetKey(switchKey) && inventoryManager.CanAddItem(itemID, amount))
            {
                //Send the item here into an empty slot. Save the current information.
                int savedID = itemID;
                int savedAmount = amount;
                RemoveItem();
                inventoryManager.AddItemToInventory(savedID, savedAmount);
            }
        }
    }

    /// <summary>
    /// Checks the id to make sure it matches the slot required ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    bool CheckCategory(int id)
    {
        char categoryID = '0';
        categoryID = id.ToString()[0];
        if(requiredID == categoryID)
        {
            return true;
        } else
        {
            return false;
        }
    }
    /*
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
            if (Input.GetKey(singleItemKey) && theItemID != 0)
            {
               // print("Here");
                if (theItemID == itemID && (1 + amount) <= theItem.GetComponent<ItemInfo>().amountLimit)
                {
                    IncreaseAmount(1);
                    inventoryManager.CI.DecreaseCurrentAmount(1);
                }
                else if (itemID == 0)
                {
                    AddItem(theItemID, 1);
                    inventoryManager.CI.DecreaseCurrentAmount(1);
                }
            }
            if (Input.GetKey(switchKey) && itemID > 0 && inventoryManager.CanAddItem(itemID, amount))
            {
                inventoryManager.AddItemToInventory(itemID, amount);
                RemoveItem();
                AddItem(theItemID, itemAmount);
                inventoryManager.CI.RemoveItem();
            }
            //If there isnt an item in the cursor but an item here.
        }
        
        else if (Input.GetKey(switchKey) && itemID > 0 && inventoryManager.CanAddItem(itemID, amount))
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
    */



}
